namespace RoadTrip.RoadTripDb.Database.Models
{
    // TODO: Implement later
    public class QuestionMultipleChoice : Question
    {
        public ICollection<string> PotentialAnswers { get; set; } = null!;
    }
}
