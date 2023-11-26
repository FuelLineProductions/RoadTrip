using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface IIndividualSubscriptionRepo : IBaseRepo<IndividualSubscription>
    {
        Task<IndividualSubscription?> Get(Guid id);

        IEnumerable<IndividualSubscription?> GetMany(ICollection<Guid> ids);

        IQueryable<IndividualSubscription> GetQueryable();
    }
}