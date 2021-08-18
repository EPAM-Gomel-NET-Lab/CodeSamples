using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AsyncVsSyncExample
{
    public interface IAsyncOperations
    {
         Task<List<T>> ToListAsync<T>(IQueryable<T> query);
    }

    public class AsyncOperations : IAsyncOperations
    {
        public async Task<List<T>> ToListAsync<T>(IQueryable<T> query) => await query.ToListAsync();
    }
}
