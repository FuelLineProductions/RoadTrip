using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
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
