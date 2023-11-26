using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class VehicleRepo(RoadTripDbContext context) : BaseRepo<Vehicle>(context), IVehicleRepo
    {
        private readonly RoadTripDbContext _context = context;

        /// <inheritdoc />
        public async Task<Vehicle?> Get(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        /// <inheritdoc />
        public IQueryable<Vehicle> GetAll()
        {
            return _context.Vehicles;
        }
    }
}