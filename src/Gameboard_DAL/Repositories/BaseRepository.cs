using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gameboard_DAL.Repositories.Models;
using Gameboard_DAL.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NoDb;

namespace Gameboard_DAL.Repositories
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

    public class BaseRepository<T, TU, TV> where T : class, IBaseItem where TU: IBaseItem, new() where TV: IBaseItem
    {
        protected readonly IBasicCommands<T> Commands;
        protected readonly HttpContext Context;
        protected readonly IBasicQueries<T> Query;
        protected readonly DALSettings Settings;
        public event EntityDeletedEventHandler OnEntityDeleted;

        protected delegate void OnDeleteHandler(object sender, string e);

        /// <summary>
        /// Just for mocking, shouldn't be used in real-world context.
        /// </summary>
        public BaseRepository()
        {

        }

        protected internal BaseRepository(
            IOptions<DALSettings> settings,
            IBasicQueries<T> query,
            IBasicCommands<T> commands,
            IHttpContextAccessor contextAccessor = null)
        {
            Settings = settings.Value;
            Context = contextAccessor?.HttpContext;
            Query = query;
            Commands = commands;
        }

        protected CancellationToken CancellationToken => Context?.RequestAborted ?? CancellationToken.None;

        public async Task<List<T>> GetAll()
        {
            var l = await Query.GetAllAsync(Settings.ProjectId, CancellationToken).ConfigureAwait(false);
            return l.ToList();
        }

        public virtual async Task<T> Get(string itemId)
        {
            return await Query.FetchAsync(Settings.ProjectId, itemId, CancellationToken);
        }

        public virtual async Task<T> Create(TV item)
        {
            item.Id = null;
            item.CreationTime = null;
            item.LastModified = null;
            var newItem = new TU();
            newItem.FromInterface(item);
            await Commands.CreateAsync(Settings.ProjectId, newItem.Id, newItem as T, CancellationToken).ConfigureAwait(false);
            return await Get(newItem.Id);
        }

        public virtual async Task<T> Update(TV item)
        {
            item.LastModified = null;
            var updatedItem = new TU();
            updatedItem.FromInterface(item);
            await Commands.UpdateAsync(Settings.ProjectId, updatedItem.Id, updatedItem as T, CancellationToken).ConfigureAwait(false);
            return await Get(updatedItem.Id);
        }

        protected virtual void EntityDeleted(EntityDeletedEventArgs e)
        {
            OnEntityDeleted?.Invoke(this, e);//Raise the event
        }

        public virtual async Task<bool> Delete(string itemId)
        {
            var item = await Get(itemId);

            if (item == null) return false;

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