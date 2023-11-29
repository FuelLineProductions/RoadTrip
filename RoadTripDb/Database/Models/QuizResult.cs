using System.ComponentModel.DataAnnotations.Schema;

namespace RoadTrip.RoadTripDb.Database.Models
{
    public class QuizResult
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public Guid GuestId { get; set; }
        public int TotalScore { get; set; }
        public int FuelUsed { get; set; }
        public int FuelGuess { get; set; }
        public int QuestionsAnswered { get; set; }
        public int QuestionsCorrect { get; set; }
    }
}