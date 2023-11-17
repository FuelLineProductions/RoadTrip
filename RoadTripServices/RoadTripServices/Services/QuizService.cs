using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.RoadTripDb.Repos;

namespace RoadTrip.RoadTripServices.RoadTripServices.Services
{
    public class QuizService(IQuizRepo quizRepo, IQuestionRepo questionRepo, IVehicleRepo vehicleRepo) : IQuizService
    {
        public async Task<ICollection<Quiz>> GetAllQuizzesForOwner(Guid ownerId)
        {
            var quizzes = quizRepo.GetManyForOwner(ownerId).ToList();
            var outputQuizzes = new List<Quiz>();
            foreach (var quiz in quizzes)
            {
                if (quiz == null)
                {
                    continue;
                }

                var assignQuiz = await GetQuiz(quiz.Id);
                outputQuizzes.Add(assignQuiz);
            }
            return outputQuizzes;
        }

        public async Task<ICollection<Quiz>> GetActiveQuizzesForOwner(Guid ownerId)
        {
            var quizzes = quizRepo.GetQueryable().Where(q => q.OwnerId.Equals(ownerId) && q.Active);
            var output = new List<Quiz>();
            foreach (var quiz in quizzes)
            {
                var assignQuiz = await GetQuiz(quiz.Id);
                output.Add(assignQuiz);
            }
            return output;
        }

        public async Task<Quiz> GetQuiz(Guid quizId)
        {
            var quiz = await quizRepo.Get(quizId);
            if (quiz == null)
            {
                ArgumentNullException.ThrowIfNull(quiz, $"Could not find quiz {quizId}");
            }

            quiz.Questions = questionRepo.GetQueryable().Where(x => x.QuizId.Equals(quizId)).ToList();

            var uniqueVehicles = quizRepo.GetQuizVehiclesQueryable().Where(v => v.QuizId.Equals(quizId))
                .Select(x => x.VehicleId).Distinct().ToList();
            var vehicles = new List<Vehicle>();
            foreach (var vehicle in uniqueVehicles)
            {
                var found = await vehicleRepo.Get(vehicle);
                if (found != null)
                {
                    vehicles.Add(found);
                }
            }
            quiz.Vehicles = vehicles;
            return quiz;
        }

        public async Task<bool> AddQuiz(Quiz quiz)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(quiz, "null not accepted");

                var added = await quizRepo.AddAsync(quiz);
                ArgumentNullException.ThrowIfNull(added, "failed to add quiz to Db");

                if (quiz.Questions.Any())
                {
                    foreach (var question in quiz.Questions)
                    {
                        question.QuizId = added.Id;
                        var addedQuestion = await questionRepo.AddAsync(question);
                        ArgumentNullException.ThrowIfNull(addedQuestion, "Failed to add question to DB");
                    }
                }

                if (quiz.Vehicles.Any())
                {
                    foreach (var vehicle in quiz.Vehicles)
                    {
                        await quizRepo.AddVehicleToQuiz(added.Id, vehicle.Id);
                    }
                }
            }
            catch (ArgumentNullException)
            {
                // TODO Log exception
                return false;
            }
            catch (Exception)
            {
                // TODO Catch all potential exceptions & log
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateQuiz(Quiz quiz)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(quiz, "invalid input");
                var existingQuiz = await quizRepo.Get(quiz.Id);
                ArgumentNullException.ThrowIfNull(existingQuiz, "existing quiz could not be found");

                existingQuiz.Active = quiz.Active;
                existingQuiz.Description = quiz.Description;
                existingQuiz.OwnerId = quiz.OwnerId;
                existingQuiz.MaximumAnswers = quiz.MaximumAnswers;
                existingQuiz.MinimumAnswers = quiz.MinimumAnswers;
                existingQuiz.Title = quiz.Title;
                existingQuiz.TotalDistance = quiz.TotalDistance;
                existingQuiz.TotalScore = quiz.TotalScore;

                var updated = await quizRepo.UpdateAsync(quiz);
                ArgumentNullException.ThrowIfNull(updated, "failed to update quiz on db");

                foreach (var question in quiz.Questions)
                {
                    var dbQuestion = await questionRepo.Get(question.Id);
                    if (dbQuestion == null)
                    {
                        await questionRepo.AddAsync(question);
                    }
                    else
                    {
                        dbQuestion.CorrectAnswer = question.CorrectAnswer;
                        dbQuestion.FuelRewardCorrectAnswer = question.FuelRewardCorrectAnswer;
                        dbQuestion.QuizId = existingQuiz.Id;
                        dbQuestion.PointsReward = question.PointsReward;
                        dbQuestion.FuelRewardIncorrectAnswer = question.FuelRewardIncorrectAnswer;
                        dbQuestion.QuestionTitle = question.QuestionTitle;
                        await questionRepo.UpdateAsync(dbQuestion);
                    }
                }

                foreach (var vehicle in quiz.Vehicles)
                {
                    var vehicleMap = quizRepo.GetQuizVehiclesQueryable()
                        .Any(x => x.VehicleId.Equals(vehicle.Id) && x.QuizId.Equals(existingQuiz.Id));
                    if (!vehicleMap)
                    {
                        await quizRepo.AddVehicleToQuiz(existingQuiz.Id, vehicle.Id);
                    }
                }

            }
            catch (ArgumentNullException)
            {
                // TODO Log exception
                return false;
            }
            catch (Exception)
            {
                // TODO Catch all potential exceptions & log
                return false;
            }
            return true;
        }

        public async Task RemoveQuestionFromQuiz(Question question)
        {
            await questionRepo.RemoveAsync(question);
        }

        public async Task RemoveVehicleFromQuiz(Quiz quiz, int vehicleId)
        {
            await quizRepo.RemoveVehicleFromQuiz(quiz.Id, vehicleId);
        }
    }
}
