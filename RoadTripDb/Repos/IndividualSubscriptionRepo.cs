using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class IndividualSubscriptionRepo(RoadTripDbContext context) : BaseRepo<IndividualSubscription>(context), IIndividualSubscriptionRepo
    {
        private readonly RoadTripDbContext _context = context;

        public async Task<IndividualSubscription?> Get(Guid id)
        {
            return await _context.IndividualSubscriptions.FindAsync(id);
        }

        public IEnumerable<IndividualSubscription?> GetMany(ICollection<Guid> ids)
        {
            foreach (var id in ids)
            {
                yield return _context.IndividualSubscriptions.Find(id);
            }
        }

        public IQueryable<IndividualSubscription> GetQueryable()
        {
            return _context.IndividualSubscriptions;
        }
    }
}