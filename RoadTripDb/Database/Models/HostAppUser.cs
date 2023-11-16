using System.ComponentModel.DataAnnotations;

namespace RoadTrip.RoadTripDb.Database.Models
{
    public class HostAppUser : RoadTripAppUser
    {
        [Key]
        public Guid RoadTripUserId { get; set; }
        public virtual ICollection<Room> OpenRooms { get; set; }
    }
}
