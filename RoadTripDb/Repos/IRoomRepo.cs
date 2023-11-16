using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
{
    public interface IRoomRepo : IBaseRepo<Room>
    {
        Task<Room?> Get(Guid id);
        IEnumerable<Room?> GetMany(ICollection<Guid> ids);
    }
}
