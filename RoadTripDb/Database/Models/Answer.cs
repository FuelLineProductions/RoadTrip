namespace RoadTrip.RoadTripDb.Database.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public Guid GuestId { get; set; }
        public Guid QuizId { get; set; }
        public string GivenAnswer { get; set; }
        public int ScoreGained { get; set; }
        public int FuelChange { get; set; }
        public int FuelGuess { get; set; }
    }
}
