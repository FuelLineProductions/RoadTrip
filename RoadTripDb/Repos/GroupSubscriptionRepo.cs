using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class GroupSubscriptionRepo(RoadTripDbContext context) : BaseRepo<GroupSubscription>(context), IGroupSubscriptionRepo
    {
        public async Task<GroupSubscription?> Get(Guid id)
        {
            return await Context.GroupSubscriptions.FindAsync(id);
        }

        public IEnumerable<GroupSubscription?> GetMany(ICollection<Guid> ids)
        {
            foreach (var id in ids)
            {
                yield return Context.GroupSubscriptions.Find(id);
            }
        }
    }
}
