using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripServices.RoadTripServices.MiddlewareModels
{
    public class AddNewHostModel
    {
        public HostAppUser HostAppUser { get; set; } = null!;
        public Guid? GroupId { get; set; } = null;
        public SubscriptionTier SubscriptionTier { get; set; } = null!;
        public bool Yearly { get; set; }
    }
}
