using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class RoomRepo(RoadTripDbContext context) : BaseRepo<Room>(context), IRoomRepo
    {
        private readonly RoadTripDbContext _context = context;
        public async Task<Room?> Get(Guid id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public IEnumerable<Room?> GetMany(ICollection<Guid> ids)
        {
            foreach (var id in ids)
            {
                yield return _context.Rooms.Find(id);
            }
        }
    }
}
