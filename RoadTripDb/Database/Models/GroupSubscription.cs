using System.ComponentModel.DataAnnotations;

namespace RoadTrip.RoadTripDb.Database.Models
{
    public class GroupSubscription : Subscription
    {
        [Key]
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public ICollection<Guid> HostsInSubscription { get; set; }
    }
}
