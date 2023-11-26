namespace RoadTrip.RoadTripDb.Database.Models
{
    public class Question
    {
        public int Id { get; set; }
        public Guid QuizId { get; set; }
        public string QuestionTitle { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!;
        public int FuelRewardCorrectAnswer { get; set; }
        public int FuelRewardIncorrectAnswer { get; set; }
        public int PointsReward { get; set; }
    }
}