using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface IRoomRepo : IBaseRepo<Room>
    {
        Task<Room?> Get(Guid id);
        IEnumerable<Room?> GetMany(ICollection<Guid> ids);
    }
}
