using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripDb.Database.Models
{
    public class Subscription
    {
        public Guid Owner { get; set; }
        public int ActiveSubscriptionTier { get; set; }
        public DateTime? SubscriptionExpiry { get; set; }
    }
}
