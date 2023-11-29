using System.ComponentModel.DataAnnotations;

namespace RoadTrip.RoadTripDb.Database.Models
{
    public class PreviousAnswer
    {
        [Key]
        public int Id { get; set; }

        public int QuestionId { get; set; }
        public Guid GuestId { get; set; }
        public string Answer { get; set; } = null!;

    }
}
