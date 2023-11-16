using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
{
    public interface IQuizRepo : IBaseRepo<Quiz>
    {
        Task<Quiz?> Get(Guid id);
        IEnumerable<Quiz?> GetMany(ICollection<Guid> ids);
        IEnumerable<Quiz?> GetManyForOwner(Guid id);
        Task AddVehicleToQuiz(Guid quizId, int vehicleId);
    }
}
