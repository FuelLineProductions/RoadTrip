namespace RoadTrip.ViewModels
{
    public class MySubscription
    {
        public Guid OwnerId { get; set; }
        public DateTime? Expiry { get; set; }
        public string TierName { get; set; } = null!;
    }
}
