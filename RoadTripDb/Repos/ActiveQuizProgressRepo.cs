using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class ActiveQuizProgressRepo(RoadTripDbContext context) : BaseRepo<ActiveQuizProgress>(context), IActiveQuizProgressRepo
    {
        private readonly RoadTripDbContext _context = context;

        public async Task<ActiveQuizProgress?> Get(int id)
        {
            return await _context.ActiveQuizProgresses.FindAsync(id);
        }

        public IEnumerable<ActiveQuizProgress>? GetManyForHost(Guid hostId)
        {
            return _context.ActiveQuizProgresses.Where(x=>x.HostId.Equals(hostId));
        }

        public IQueryable<ActiveQuizProgress> GetQueryable()
        {
            return _context.ActiveQuizProgresses;
        }

        
    }
}
