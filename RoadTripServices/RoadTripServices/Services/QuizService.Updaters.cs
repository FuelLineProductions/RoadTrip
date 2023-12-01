using RoadTrip.RoadTripDb.Database.Models;
using Serilog;

namespace RoadTrip.RoadTripServices.RoadTripServices.Services
{
    public partial class QuizService
    {
        /// <inheritdoc/>
        public async Task<bool> UpdateQuiz(Quiz quiz)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(quiz, "invalid input");
                var existingQuiz = await _quizRepo.Get(quiz.Id);
                ArgumentNullException.ThrowIfNull(existingQuiz, "existing quiz could not be found");

                existingQuiz.Active = quiz.Active;
                existingQuiz.Description = quiz.Description;
                existingQuiz.OwnerId = quiz.OwnerId;
                existingQuiz.MaximumAnswers = quiz.MaximumAnswers;
                existingQuiz.MinimumAnswers = quiz.MinimumAnswers;
                existingQuiz.Title = quiz.Title;
                existingQuiz.TotalDistance = quiz.TotalDistance;
                existingQuiz.TotalScore = quiz.TotalScore;

                var updated = await _quizRepo.UpdateAsync(quiz);
                ArgumentNullException.ThrowIfNull(updated, "failed to update quiz on db");

                foreach (var question in quiz.Questions)
                {
                    var dbQuestion = await _questionRepo.Get(question.Id);
                    if (dbQuestion == null)
                    {
                        await _questionRepo.AddAsync(question);
                    }
                    else
                    {
                        dbQuestion.CorrectAnswer = question.CorrectAnswer;
                        dbQuestion.FuelRewardCorrectAnswer = question.FuelRewardCorrectAnswer;
                        dbQuestion.QuizId = existingQuiz.Id;
                        dbQuestion.PointsReward = question.PointsReward;
                        dbQuestion.FuelRewardIncorrectAnswer = question.FuelRewardIncorrectAnswer;
                        dbQuestion.QuestionTitle = question.QuestionTitle;
                        await _questionRepo.UpdateAsync(dbQuestion);
                    }
                }

                foreach (var vehicle in quiz.Vehicles)
                {
                    var vehicleMap = _quizRepo.GetQuizVehiclesQueryable()
                        .Any(x => x.VehicleId.Equals(vehicle.Id) && x.QuizId.Equals(existingQuiz.Id));
                    if (!vehicleMap)
                    {
                        await _quizRepo.AddVehicleToQuiz(existingQuiz.Id, vehicle.Id);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                Log.Error("Null exception on update quiz {ex}", ex);
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("Unhandled exception on update quiz {ex}", ex);
                return false;
            }
            return true;
        }
    }
}