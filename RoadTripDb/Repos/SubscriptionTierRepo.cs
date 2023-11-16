using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
{
    public class SubscriptionTierRepo(RoadTripDbContext context) : BaseRepo<SubscriptionTier>(context), ISubscriptionTierRepo
    {
        public async Task<SubscriptionTier?> Get(int id)
        {
            return await Context.SubscriptionTiers.FindAsync(id);
        }

        public IEnumerable<SubscriptionTier?> GetMany(ICollection<int> ids)
        {
            foreach (var id in ids)
            {
                yield return Context.SubscriptionTiers.Find(id);
            }
        }
    }
}
