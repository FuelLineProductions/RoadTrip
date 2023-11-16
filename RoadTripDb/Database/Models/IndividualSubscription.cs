using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripDb.Database.Models
{
    public class IndividualSubscription : Subscription
    {
        [Key]
        public Guid IndividualId { get; set; }
    }
}
