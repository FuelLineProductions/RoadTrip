using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class QuizRepo(RoadTripDbContext context) : BaseRepo<Quiz>(context), IQuizRepo
    {
        private readonly RoadTripDbContext _context = context;

        public async Task<Quiz?> Get(Guid id)
        {
            return await _context.Quizzes.FindAsync(id);
        }

        /// <inheritdoc />
        public IEnumerable<Quiz>? GetMany(ICollection<Guid> ids)
        {
            foreach (var id in ids)
            {
                var quiz = _context.Quizzes.Find(id);
                if (quiz == null)
                {
                    continue;
                }
                yield return quiz;
            }
        }

        /// <inheritdoc />
        public IEnumerable<Quiz>? GetManyForOwner(Guid id)
        {
            return _context.Quizzes.Where(question => question.OwnerId.Equals(id));
        }

        /// <inheritdoc />
        public async Task AddVehicleToQuiz(Guid quizId, int vehicleId)
        {
            await _context.QuizVehicles.AddAsync(new QuizVehicles() { QuizId = quizId, VehicleId = vehicleId });
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task AddRangeVehicleToQuiz(List<QuizVehicles> quizVehicles)
        {
            await _context.QuizVehicles.AddRangeAsync(quizVehicles);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public IQueryable<Quiz> GetQueryable()
        {
            return _context.Quizzes;
        }

        /// <inheritdoc />
        public IQueryable<QuizVehicles> GetQuizVehiclesQueryable()
        {
            return _context.QuizVehicles;
        }

        /// <inheritdoc />
        public async Task RemoveVehicleFromQuiz(Guid quizId, int vehicleId)
        {
            var matches = _context.QuizVehicles.Where(x => x.QuizId.Equals(quizId) && vehicleId.Equals(vehicleId))
                .ToList();

            _context.QuizVehicles.RemoveRange(matches);
            await _context.SaveChangesAsync();
        }
    }
}