using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripDb.Database.Models
{
    public class GroupSubscription : Subscription
    {
        [Key]
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public ICollection<Guid> HostsInSubscription { get; set; }
    }
}
