using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class SubscriptionTierRepo(RoadTripDbContext context) : BaseRepo<SubscriptionTier>(context), ISubscriptionTierRepo
    {
        private readonly RoadTripDbContext _context = context;
        public async Task<SubscriptionTier?> Get(int id)
        {
            return await _context.SubscriptionTiers.FindAsync(id);
        }

        public IEnumerable<SubscriptionTier?> GetMany(ICollection<int> ids)
        {
            foreach (var id in ids)
            {
                yield return _context.SubscriptionTiers.Find(id);
            }
        }

        public IQueryable<SubscriptionTier> GetQueryable()
        {
            return _context.SubscriptionTiers;
        }
    }
}
