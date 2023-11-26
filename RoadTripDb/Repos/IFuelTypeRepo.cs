using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface IFuelTypeRepo : IBaseRepo<FuelType>
    {
        Task<FuelType?> Get(int id);

        IEnumerable<FuelType?> GetMany(ICollection<int> ids);
    }
}