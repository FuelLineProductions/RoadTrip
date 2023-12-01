namespace RoadTrip.RoadTripDb.Database.Models
{
    public class GuestRequestJoinQuiz
    {
        public int Id { get; set; }
        public Guid QuizId { get; set; }
        public Guid GuestId { get; set; }
        public DateTime? RequestedAt { get; set; }
        public DateTime? ExpiredAt { get; set; }
    }
}
