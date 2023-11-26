using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface IVehicleRepo : IBaseRepo<Vehicle>
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Vehicle?> Get(int id);

        /// <summary>
        /// Get a queryable vehicle
        /// </summary>
        /// <returns></returns>
        IQueryable<Vehicle> GetAll();
    }
}