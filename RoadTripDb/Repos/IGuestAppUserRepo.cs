using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
{
    public interface IGuestAppUserRepo: IBaseRepo<GuestAppUser>
    {
        Task<GuestAppUser?> Get(Guid id);
        IEnumerable<GuestAppUser?> GetRange(ICollection<Guid> ids);
    }
}
