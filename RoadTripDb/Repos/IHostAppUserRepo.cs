using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface IHostAppUserRepo : IBaseRepo<HostAppUser>
    {
        /// <summary>
        /// Get host app user by user ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<HostAppUser?> GetHostAppUser(Guid id);
        /// <summary>
        /// Get host app users by user ID collection
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        IEnumerable<HostAppUser?> GetHostAppUsers(ICollection<Guid> ids);
    }
}
