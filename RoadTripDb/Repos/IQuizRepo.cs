using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface IQuizRepo : IBaseRepo<Quiz>
    {
        Task<Quiz?> Get(Guid id);
        IEnumerable<Quiz?> GetMany(ICollection<Guid> ids);
        IEnumerable<Quiz?> GetManyForOwner(Guid id);
        Task AddVehicleToQuiz(Guid quizId, int vehicleId);
    }
}
