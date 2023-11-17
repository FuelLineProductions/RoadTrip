using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadTrip.RoadTripDb.Database.Models
{
    public class GroupSubscription : Subscription
    {
        [Key]
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }

        [NotMapped]
        public virtual ICollection<Guid> HostsInSubscription { get; set; }
    }
}
