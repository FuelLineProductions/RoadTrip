using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
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
