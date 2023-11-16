using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
{
    public interface IIndividualSubscriptionRepo : IBaseRepo<IndividualSubscription>
    {
        Task<IndividualSubscription?> Get(Guid id);
        IEnumerable<IndividualSubscription?> GetMany(ICollection<Guid> ids);
        IQueryable<IndividualSubscription> GetQueryable();
    }
}
