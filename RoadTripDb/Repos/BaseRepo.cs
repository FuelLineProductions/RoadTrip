using RoadTrip.RoadTripDb.Database;

namespace RoadTrip.RoadTripDb.Repos
{
    public class BaseRepo<T>(RoadTripDbContext context) : IBaseRepo<T>
    {
        private readonly RoadTripDbContext _context = context;

        /// <inheritdoc/>
        public async Task<T> AddAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc/>
        public async Task RemoveAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<T> UpdateAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc/>
        public async Task RemoveRangeAsync(ICollection<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            _context.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<ICollection<T>> UpdateRangeAsync(ICollection<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            _context.UpdateRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        /// <inheritdoc/>
        public async Task<ICollection<T>> AddRangeAsync(ICollection<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            _context.AddRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }
    }
}