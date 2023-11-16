using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
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
