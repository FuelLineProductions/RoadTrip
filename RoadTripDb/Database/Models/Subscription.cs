namespace RoadTrip.RoadTripDb.Database.Models
{
    public class Subscription
    {
        public Guid Owner { get; set; }
        public int ActiveSubscriptionTier { get; set; }
        public DateTime? SubscriptionExpiry { get; set; }
    }
}
