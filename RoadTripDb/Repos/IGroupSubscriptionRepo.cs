using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
{
    public interface IGroupSubscriptionRepo : IBaseRepo<GroupSubscription>
    {
        Task<GroupSubscription?> Get(Guid id);
        IEnumerable<GroupSubscription?> GetMany(ICollection<Guid> ids);
    }
}
