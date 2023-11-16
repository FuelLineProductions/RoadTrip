using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
{
    public class RoomRepo(RoadTripDbContext context) : BaseRepo<Room>(context) , IRoomRepo
    {
        public async Task<Room?> Get(Guid id)
        {
            return await Context.Rooms.FindAsync(id);
        }

        public IEnumerable<Room?> GetMany(ICollection<Guid> ids)
        {
            foreach (var id in ids)
            {
                yield return Context.Rooms.Find(id);
            }
        }
    }
}
