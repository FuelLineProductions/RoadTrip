using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class VehicleRepo(RoadTripDbContext context) : BaseRepo<Vehicle>(context), IVehicleRepo
    {
        /// <inheritdoc />
        public async Task<Vehicle?> Get(int id)
        {
            return await Context.Vehicles.FindAsync(id);
        }

        /// <inheritdoc />
        public IQueryable<Vehicle> GetAll()
        {
            return Context.Vehicles;
        }
    }
}
