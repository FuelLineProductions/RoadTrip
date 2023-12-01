using RoadTrip.RoadTripDb.Database.Models;
using System.Collections.Immutable;

namespace RoadTrip.RoadTripServices.RoadTripServices.Services
{
    public partial class QuizService
    {
        /// <inheritdoc/>
        public async Task<ICollection<Quiz>> GetActiveQuizzesForOwner(Guid ownerId)
        {
            var quizzes = _quizRepo.GetQueryable().Where(q => q.OwnerId.Equals(ownerId) && q.Active);
            var output = new List<Quiz>();
            foreach (var quiz in quizzes)
            {
                var assignQuiz = await GetQuiz(quiz.Id);
                if (assignQuiz != null)
                    output.Add(assignQuiz);
            }
            return output;
        }

        /// <inheritdoc/>
        public async Task<ICollection<Quiz>?> GetAllQuizzesForOwner(Guid? ownerId)
        {
            if (!ownerId.HasValue || ownerId == Guid.Empty)
            {
                return null;
            }
            var quizzes = _quizRepo.GetManyForOwner(ownerId.Value)?.ToList();
            var outputQuizzes = new List<Quiz>();
            if (quizzes == null)
            {
                return null;
            }
            foreach (var quiz in quizzes)
            {
                if (quiz == null)
                {
                    continue;
                }

                var assignQuiz = await GetQuiz(quiz.Id);
                if (assignQuiz != null)
                    outputQuizzes.Add(assignQuiz);
            }
            return outputQuizzes;
        }

        /// <inheritdoc/>
        public async Task<Quiz> GetInviteOnlyQuiz()
        {
            // TODO Make and implement a way of sending an invite only quiz option
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IImmutableList<Quiz>> GetOpenActiveQuizzes()
        {
            // TODO: Add functionality to differenciate between open to anyone, and invite only quizzes
            return [.. _quizRepo.GetQueryable().Where(x => x.Active)];
        }

        /// <inheritdoc/>
        public async Task<Quiz?> GetQuiz(Guid? quizId)
        {
            if (!quizId.HasValue || quizId == Guid.Empty)
            {
                return null;
            }

            var quiz = await _quizRepo.Get(quizId.Value);
            if (quiz == null)
            {
                ArgumentNullException.ThrowIfNull(quiz, $"Could not find quiz {quizId}");
            }

            quiz.Questions = _questionRepo.GetQueryable().Where(x => x.QuizId.Equals(quizId)).ToList();

            var uniqueVehicles = _quizRepo.GetQuizVehiclesQueryable().Where(v => v.QuizId.Equals(quizId))
                .Select(x => x.VehicleId).Distinct().ToList();
            var vehicles = new List<Vehicle>();
            foreach (var vehicle in uniqueVehicles)
            {
                var found = await _vehicleRepo.Get(vehicle);
                if (found != null)
                {
                    vehicles.Add(found);
                }
            }
            quiz.Vehicles = vehicles;
            return quiz;
        }
    }
}