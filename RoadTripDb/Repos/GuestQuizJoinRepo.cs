using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class GuestQuizJoinRepo(RoadTripDbContext context) : BaseRepo<GuestRequestJoinQuiz>(context), IGuestQuizJoinRepo
    {
        private readonly RoadTripDbContext _context = context;

        public async Task<GuestRequestJoinQuiz?> Get(int id)
        {
            return await _context.GuestRequestJoinQuizzes.FindAsync(id);
        }

        public IQueryable<GuestRequestJoinQuiz> GetQueryable()
        {
            return _context.GuestRequestJoinQuizzes;
        }
    }
}
