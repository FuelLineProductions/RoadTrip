using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
{
    public class VehicleRepo(RoadTripDbContext context)  : BaseRepo<Vehicle>(context), IVehicleRepo
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
