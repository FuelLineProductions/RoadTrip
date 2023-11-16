using RoadTripDb.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripServices.MiddlewareModels
{
    public class AddNewHostModel
    {
        public HostAppUser HostAppUser { get; set; } = null!;
        public Guid? GroupId { get; set; } = null;
        public SubscriptionTier SubscriptionTier { get; set; } = null!;
        public bool Yearly { get; set; }
    }
}
