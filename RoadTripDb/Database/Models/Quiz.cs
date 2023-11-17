namespace RoadTrip.RoadTripDb.Database.Models
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int TotalScore { get; set; }
        public int TotalDistance { get; set; }
        public int MinimumAnswers { get; set; }
        public int MaximumAnswers { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Question> Questions { get; set; } = null!;
        public virtual ICollection<Vehicle> Vehicles { get; set; } = null!;
    }
}
