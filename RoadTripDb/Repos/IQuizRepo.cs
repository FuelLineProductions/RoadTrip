using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface IQuizRepo : IBaseRepo<Quiz>
    {
        Task<Quiz?> Get(Guid id);

        IEnumerable<Quiz?> GetMany(ICollection<Guid> ids);

        IEnumerable<Quiz?> GetManyForOwner(Guid id);

        Task AddVehicleToQuiz(Guid quizId, int vehicleId);

        Task AddRangeVehicleToQuiz(List<QuizVehicles> quizVehicles);

        IQueryable<Quiz> GetQueryable();

        IQueryable<QuizVehicles> GetQuizVehiclesQueryable();

        Task RemoveVehicleFromQuiz(Guid quizId, int vehicleId);
    }
}