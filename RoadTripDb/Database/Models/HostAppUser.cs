using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadTrip.RoadTripDb.Database.Models
{
    public class HostAppUser : RoadTripAppUser
    {
        [Key]
        public Guid RoadTripUserId { get; set; }

        [NotMapped]
        public virtual ICollection<Room> OpenRooms { get; set; }
    }
}
