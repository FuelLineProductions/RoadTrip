using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class FuelTypeRepo(RoadTripDbContext context) : BaseRepo<FuelType>(context), IFuelTypeRepo
    {
        private readonly RoadTripDbContext _context = context;

        public async Task<FuelType?> Get(int id)
        {
            return await _context.FuelTypes.FindAsync(id);
        }

        public IEnumerable<FuelType?> GetMany(ICollection<int> ids)
        {
            foreach (var id in ids)
            {
                yield return _context.FuelTypes.Find(id);
            }
        }
    }
}
