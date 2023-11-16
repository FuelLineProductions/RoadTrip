using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
{
    public interface IFuelTypeRepo : IBaseRepo<FuelType>
    {
        Task<FuelType?> Get(int id);
        IEnumerable<FuelType?> GetMany(ICollection<int> ids);
    }
}
