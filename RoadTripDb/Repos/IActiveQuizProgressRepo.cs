using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface IActiveQuizProgressRepo : IBaseRepo<ActiveQuizProgress>
    {
        Task<ActiveQuizProgress?> Get(int id);
        IEnumerable<ActiveQuizProgress>? GetManyForHost(Guid hostId);
        IQueryable<ActiveQuizProgress> GetQueryable();
    }
}
