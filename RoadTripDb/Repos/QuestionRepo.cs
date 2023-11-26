using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Repos
{
    public class QuestionRepo(RoadTripDbContext context) : BaseRepo<Question>(context), IQuestionRepo
    {
        private readonly RoadTripDbContext _context = context;

        public async Task<Question?> Get(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public IEnumerable<Question?> GetMany(ICollection<int> ids)
        {
            foreach (var id in ids)
            {
                yield return _context.Questions.Find(id);
            }
        }

        public IEnumerable<Question?> GetMany(ICollection<Guid> ids)
        {
            var questions = new List<Question>();
            foreach (var id in ids)
            {
                questions.AddRange(_context.Questions.Where(question => question.QuizId.Equals(id)));
            }
            return questions;
        }

        public IQueryable<Question> GetQueryable()
        {
            return _context.Questions;
        }
    }
}