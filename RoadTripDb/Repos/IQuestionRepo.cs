using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface IQuestionRepo : IBaseRepo<Question>
    {
        Task<Question?> Get(int id);

        IEnumerable<Question?> GetMany(ICollection<int> ids);

        IEnumerable<Question?> GetMany(ICollection<Guid> ids);

        IQueryable<Question> GetQueryable();
    }
}