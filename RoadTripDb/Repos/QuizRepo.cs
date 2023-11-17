using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class QuizRepo(RoadTripDbContext context) : BaseRepo<Quiz>(context), IQuizRepo
    {
        public async Task<Quiz?> Get(Guid id)
        {
            return await Context.Quizzes.FindAsync(id);
        }

        public IEnumerable<Quiz?> GetMany(ICollection<Guid> ids)
        {
            foreach (var id in ids)
            {
                yield return Context.Quizzes.Find(id);
            }
        }

        public IEnumerable<Quiz?> GetManyForOwner(Guid id)
        {
            return Context.Quizzes.Where(question => question.OwnerId.Equals(id));
        }

        public async Task AddVehicleToQuiz(Guid quizId, int vehicleId)
        {
            await Context.QuizVehicles.AddAsync(new QuizVehicles() { QuizId = quizId, VehicleId = vehicleId });
            await Context.SaveChangesAsync();
        }

        public IQueryable<Quiz> GetQueryable()
        {
            return Context.Quizzes;
        }

        public IQueryable<QuizVehicles> GetQuizVehiclesQueryable()
        {
            return Context.QuizVehicles;
        }

        public async Task RemoveVehicleFromQuiz(Guid quizId, int vehicleId)
        {
            var matches = Context.QuizVehicles.Where(x => x.QuizId.Equals(quizId) && vehicleId.Equals(vehicleId))
                .ToList();

            Context.QuizVehicles.RemoveRange(matches);
            await context.SaveChangesAsync();
        }
    }
}
