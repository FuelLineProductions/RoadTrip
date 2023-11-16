using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database;

namespace RoadTripDb.Repos
{
    public class BaseRepo<T>(RoadTripDbContext context) : IBaseRepo<T>
    {
        public readonly RoadTripDbContext Context = context;

        /// <inheritdoc/>
        public async Task AddAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task RemoveAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            Context.Update(entity);
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task RemoveRangeAsync(ICollection<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            Context.RemoveRange(entities);
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateRangeAsync(ICollection<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            Context.UpdateRange(entities);
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task AddRangeAsync(ICollection<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            Context.AddRange(entities);
            await Context.SaveChangesAsync();
        }
    }
}
