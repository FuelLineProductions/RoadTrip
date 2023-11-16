using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class IndividualSubscriptionRepo(RoadTripDbContext context) : BaseRepo<IndividualSubscription>(context), IIndividualSubscriptionRepo
    {
        public async Task<IndividualSubscription?> Get(Guid id)
        {
            return await Context.IndividualSubscriptions.FindAsync(id);
        }

        public IEnumerable<IndividualSubscription?> GetMany(ICollection<Guid> ids)
        {
            foreach (var id in ids)
            {
                yield return Context.IndividualSubscriptions.Find(id);
            }
        }

        public IQueryable<IndividualSubscription> GetQueryable()
        {
            return Context.IndividualSubscriptions;
        }
    }
}
