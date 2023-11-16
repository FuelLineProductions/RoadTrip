namespace RoadTrip.RoadTripDb.Database.Models
{
    public class FuelType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CostPerQuestion { get; set; }
    }
}
