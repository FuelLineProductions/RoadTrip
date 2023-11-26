using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class HostAppUserRepo(RoadTripDbContext context) : BaseRepo<HostAppUser>(context), IHostAppUserRepo
    {
        private readonly RoadTripDbContext _context = context;

        /// <inheritdoc />
        public async Task<HostAppUser?> GetHostAppUser(Guid id)
        {
            return await _context.HostAppUsers.FindAsync(id);
        }

        /// <inheritdoc />
        public IEnumerable<HostAppUser?> GetHostAppUsers(ICollection<Guid> ids)
        {
            foreach (var id in ids)
            {
                yield return _context.HostAppUsers.Find(id);
            }
        }
    }
}