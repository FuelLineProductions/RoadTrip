using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface IGroupSubscriptionRepo : IBaseRepo<GroupSubscription>
    {
        Task<GroupSubscription?> Get(Guid id);

        IEnumerable<GroupSubscription?> GetMany(ICollection<Guid> ids);
    }
}