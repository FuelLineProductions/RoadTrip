using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
{
    public interface ISubscriptionTierRepo : IBaseRepo<SubscriptionTier>
    {
        Task<SubscriptionTier?> Get(int id);
        IEnumerable<SubscriptionTier?> GetMany(ICollection<int> ids);

    }
}
