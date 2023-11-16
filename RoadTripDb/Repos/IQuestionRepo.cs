using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Repos
{
    public interface IQuestionRepo : IBaseRepo<Question>
    {
        Task<Question?> Get(int id);
        IEnumerable<Question?> GetMany(ICollection<int> ids);
        IEnumerable<Question?> GetMany(ICollection<Guid> ids);
    }
}
