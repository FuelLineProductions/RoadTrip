using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class GuestAppUserRepo(RoadTripDbContext context) : BaseRepo<GuestAppUser>(context), IGuestAppUserRepo
    {
        private readonly RoadTripDbContext _context = context;
        public async Task<GuestAppUser?> Get(Guid id)
        {
            return await _context.GuestAppUsers.FindAsync(id);
        }

        public IEnumerable<GuestAppUser?> GetRange(ICollection<Guid> ids)
        {
            foreach (var id in ids)
            {
                yield return _context.GuestAppUsers.Find(id);
            }
        }
    }
}
