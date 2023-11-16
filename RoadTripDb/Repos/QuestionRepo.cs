using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class QuestionRepo(RoadTripDbContext context) : BaseRepo<Question>(context), IQuestionRepo
    {
        public async Task<Question?> Get(int id)
        {
            return await Context.Questions.FindAsync(id);
        }

        public IEnumerable<Question?> GetMany(ICollection<int> ids)
        {
            foreach (var id in ids)
            {
                yield return Context.Questions.Find(id);
            }
        }

        public IEnumerable<Question?> GetMany(ICollection<Guid> ids)
        {
            var questions = new List<Question>();
            foreach (var id in ids)
            {
                questions.AddRange(Context.Questions.Where(question => question.QuizId.Equals(id)));
            }
            return questions;
        }
    }
}
