namespace RoadTrip.RoadTripDb.Database.Models
{
    // TODO: Implement later
    public class QuestionMaths : Question
    {
        public MathsOperatorsAllowed AllowedOperators { get; set; } = null!;
        public double MathsAnswer { get; set; }
    }
}