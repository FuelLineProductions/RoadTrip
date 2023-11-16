using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class HostAppUserRepo(RoadTripDbContext context) : BaseRepo<HostAppUser>(context), IHostAppUserRepo
    {
        /// <inheritdoc />
        public async Task<HostAppUser?> GetHostAppUser(Guid id)
        {
            return await Context.HostAppUsers.FindAsync(id);
        }

        /// <inheritdoc />
        public IEnumerable<HostAppUser?> GetHostAppUsers(ICollection<Guid> ids)
        {
            foreach (var id in ids)
            {
                yield return Context.HostAppUsers.Find(id);
            }
        }

    }
}
