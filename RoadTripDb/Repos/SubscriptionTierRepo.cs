using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
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

        public IQueryable<SubscriptionTier> GetQueryable()
        {
            return Context.SubscriptionTiers;
        }
    }
}
