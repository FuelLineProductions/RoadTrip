using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
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
