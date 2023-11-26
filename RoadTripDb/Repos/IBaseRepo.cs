namespace RoadTrip.RoadTripDb.Repos
{
    public interface IBaseRepo<T>
    {
        /// <summary>
        /// Add a single entity of <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Update a single entity of <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Delete a single entity of <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task RemoveAsync(T entity);

        /// <summary>
        /// Add a range of entities of <typeparamref name="T"/>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<ICollection<T>> AddRangeAsync(ICollection<T> entities);

        /// <summary>
        /// Update a range of entities of <typeparamref name="T"/>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<ICollection<T>> UpdateRangeAsync(ICollection<T> entities);

        /// <summary>
        /// Remove a ranage of entities of <typeparamref name="T"/>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task RemoveRangeAsync(ICollection<T> entities);
    }
}