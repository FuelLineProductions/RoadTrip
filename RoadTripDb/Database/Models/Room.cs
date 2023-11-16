using System.ComponentModel.DataAnnotations;

namespace RoadTrip.RoadTripDb.Database.Models
{
    public class Room
    {
        [Key]
        public Guid RoomId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int MaxUsers { get; set; }
        public int CurrentUserCount { get; set; }
    }
}
