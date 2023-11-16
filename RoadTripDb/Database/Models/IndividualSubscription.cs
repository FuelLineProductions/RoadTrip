using System.ComponentModel.DataAnnotations;

namespace RoadTrip.RoadTripDb.Database.Models
{
    public class IndividualSubscription : Subscription
    {
        [Key]
        public Guid IndividualId { get; set; }
    }
}
