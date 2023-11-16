using System.ComponentModel.DataAnnotations;

namespace RoadTrip.RoadTripDb.Database.Models
{
    public class GuestAppUser : RoadTripAppUser
    {
        [Key]
        public Guid GuestId { get; set; }
        public Guid HostId { get; set; }
        public Guid? ActiveRoomId { get; set; }
        public ICollection<Guid> RoomInvites { get; set; }
    }
}
