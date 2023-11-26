using System.ComponentModel.DataAnnotations.Schema;

namespace RoadTrip.RoadTripDb.Database.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int FuelCapacity { get; set; }
        public int FuelTypeId { get; set; }

        [NotMapped]
        public virtual FuelType? FuelType { get; set; } = null!;
    }
}