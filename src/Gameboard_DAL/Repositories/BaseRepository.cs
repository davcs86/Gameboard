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
    public class BaseRepository<T, TU> where T : class, IBaseItem where TU: IBaseItem, new()
    {
        protected readonly IBasicCommands<T> Commands;
        protected readonly HttpContext Context;
        protected readonly IBasicQueries<T> Query;
        protected readonly DALSettings Settings;

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
            var list = l.ToList();

            return list;
        }

        public async Task<T> Get(string itemId)
        {
            var allItems = await GetAll().ConfigureAwait(false);
            return allItems.FirstOrDefault(p => p.Id == itemId);
        }

        public async Task<T> Create(TU item)
        {
            item.Id = null;
            item.CreationTime = null;
            item.LastModified = null;
            var newItem = new TU();
            newItem.FromInterface(item);
            await Commands.CreateAsync(Settings.ProjectId, newItem.Id, newItem as T, CancellationToken).ConfigureAwait(false);
            return await Get(newItem.Id);
        }

        public async Task<T> Update(TU item)
        {
            item.LastModified = null;
            var updatedItem = new TU();
            updatedItem.FromInterface(item);
            await Commands.UpdateAsync(Settings.ProjectId, updatedItem.Id, updatedItem as T, CancellationToken).ConfigureAwait(false);
            return await Get(updatedItem.Id);
        }

        public async Task<bool> Delete(string itemId)
        {
            var item = await Query.FetchAsync(Settings.ProjectId, itemId, CancellationToken.None);

            if (item == null) return false;

            var items = await GetAll().ConfigureAwait(false);
            await
                Commands.DeleteAsync(Settings.ProjectId, itemId, CancellationToken)
                    .ConfigureAwait(false);
            return items.Remove(item);
        }
    }
}