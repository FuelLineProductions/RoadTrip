namespace RoadTrip.RoadTripDb.Database.Models
{
    public class QuizVehicles
    {
        public int Id { get; set; }
        public Guid QuizId { get; set; }
        public int VehicleId { get; set; }
    }
}
