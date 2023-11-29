using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadTrip.RoadTripDb.Database.Models
{
    public class GuestAppUser : RoadTripAppUser
    {
        [Key]
        public Guid GuestId { get; set; }
    }
}