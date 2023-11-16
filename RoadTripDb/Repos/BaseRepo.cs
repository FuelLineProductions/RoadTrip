using RoadTrip.RoadTripDb.Database;

namespace RoadTrip.RoadTripDb.Repos
{
    public class BaseRepo<T>(RoadTripDbContext context) : IBaseRepo<T>
    {
        public readonly RoadTripDbContext Context = context;

        /// <inheritdoc/>
        public async Task<T> AddAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc/>
        public async Task RemoveAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<T> UpdateAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            Context.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc/>
        public async Task RemoveRangeAsync(ICollection<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            Context.RemoveRange(entities);
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<ICollection<T>> UpdateRangeAsync(ICollection<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            Context.UpdateRange(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

        /// <inheritdoc/>
        public async Task<ICollection<T>> AddRangeAsync(ICollection<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            Context.AddRange(entities);
            await Context.SaveChangesAsync();
            return entities;
        }
    }
}
