using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface ISubscriptionTierRepo : IBaseRepo<SubscriptionTier>
    {
        Task<SubscriptionTier?> Get(int id);

        IEnumerable<SubscriptionTier?> GetMany(ICollection<int> ids);

        IQueryable<SubscriptionTier> GetQueryable();
    }
}