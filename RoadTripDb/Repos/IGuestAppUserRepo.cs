using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public interface IGuestAppUserRepo : IBaseRepo<GuestAppUser>
    {
        Task<GuestAppUser?> Get(Guid id);
        IEnumerable<GuestAppUser?> GetRange(ICollection<Guid> ids);
    }
}
