using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gameboard_DAL.Entities;
using Gameboard_DAL.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NoDb;

namespace Gameboard_DAL
{
    public class EntityDeletedEventArgs : EventArgs
    {
        public EntityDeletedEventArgs(string entityId)
        {
            EntityId = entityId;
        }
        public string EntityId { get; }
    }

    public delegate void EntityDeletedEventHandler(object sender, EntityDeletedEventArgs e);

    public class EntityRetrievedEventArgs<T> : EventArgs
    {
        public EntityRetrievedEventArgs(ref T entity)
        {
            EntityInstance = entity;
        }
        public T EntityInstance { get; }
    }

    public delegate void EntityRetrievedEventHandler<T>(object sender, EntityRetrievedEventArgs<T> e);

    public class DbRepository<T> where T: class, IBaseItem, new()
    {
        protected readonly IBasicCommands<T> Commands;
        protected readonly HttpContext Context;
        protected readonly IBasicQueries<T> Query;
        protected readonly DALSettings Settings;
        public event EntityDeletedEventHandler OnEntityDeleted;
        public event EntityRetrievedEventHandler<T> OnEntityRetrieved;

        protected delegate void OnDeleteHandler(object sender, string e);
        protected delegate void OnRetrieveHandler(object sender, T e);

        /// <summary>
        /// Just for mocking purpose, shouldn't be used in real-world context.
        /// </summary>
        public DbRepository()
        {

        }

        protected internal DbRepository(IOptions<DALSettings> settings, IBasicQueries<T> query, IBasicCommands<T> commands, IHttpContextAccessor contextAccessor = null)
        {
            Settings = settings.Value;
            Context = contextAccessor?.HttpContext;
            Query = query;
            Commands = commands;
        }

        protected CancellationToken CancellationToken => Context?.RequestAborted ?? CancellationToken.None;

        public async Task<IEnumerable<T>> GetAll()
        {
            var l = await Query.GetAllAsync(Settings.ProjectId, CancellationToken);
            var f = l.ToArray();
            for (int i = 0; i < f.Length; i++ ){ 
                EntityRetrievedEventArgs<T> e = new EntityRetrievedEventArgs<T>(ref f[i]);
                EntityRetrieved(e);
            }
            return f;
        }

        public virtual async Task<T> Get(string itemId)
        {
            var i = await Query.FetchAsync(Settings.ProjectId, itemId, CancellationToken);
            EntityRetrievedEventArgs<T> e = new EntityRetrievedEventArgs<T>(ref i);
            EntityRetrieved(e);
            return i;
        }

        public virtual async Task<T> Create(T item)
        {
            item.Id = null;
            item.Id = item.Id; // force a new key
            item.CreationTime = null;
            item.LastModified = null;
            await Commands.CreateAsync(Settings.ProjectId, item.Id, item, CancellationToken).ConfigureAwait(false);
            return await Get(item.Id);
        }

        public virtual async Task<T> Update(T item)
        {
            item.LastModified = null;
            await Commands.UpdateAsync(Settings.ProjectId, item.Id, item, CancellationToken).ConfigureAwait(false);
            return await Get(item.Id);
        }

        protected virtual void EntityRetrieved(EntityRetrievedEventArgs<T> e)
        {
            OnEntityRetrieved?.Invoke(this, e); //Raise the event
        }

        protected virtual void EntityDeleted(EntityDeletedEventArgs e)
        {
            OnEntityDeleted?.Invoke(this, e); //Raise the event
        }

        public virtual async Task<bool> Delete(string itemId)
        {
            var item = await Get(itemId);

            if (item == null) return false; // if it doesn't exist

            try
            {
                await Commands.DeleteAsync(Settings.ProjectId, itemId, CancellationToken).ConfigureAwait(false);
                EntityDeletedEventArgs e = new EntityDeletedEventArgs(itemId);
                EntityDeleted(e);
                return true;
            }
            catch (Exception)
            {
                //
            }
            return false;
        }
    }
}