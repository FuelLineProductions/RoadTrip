using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface IGuestQuizJoinRepo : IBaseRepo<GuestRequestJoinQuiz>
    {
        Task<GuestRequestJoinQuiz?> Get(int id);
        IQueryable<GuestRequestJoinQuiz> GetQueryable();
    }
}
