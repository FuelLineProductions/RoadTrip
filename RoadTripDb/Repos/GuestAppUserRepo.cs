using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class GuestAppUserRepo(RoadTripDbContext context) : BaseRepo<GuestAppUser>(context), IGuestAppUserRepo
    {
        public async Task<GuestAppUser?> Get(Guid id)
        {
            return await Context.GuestAppUsers.FindAsync(id);
        }

        public IEnumerable<GuestAppUser?> GetRange(ICollection<Guid> ids)
        {
            foreach (var id in ids)
            {
                yield return Context.GuestAppUsers.Find(id);
            }
        }
    }
}
