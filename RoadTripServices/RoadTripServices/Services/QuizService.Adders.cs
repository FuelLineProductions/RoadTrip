using RoadTrip.RoadTripDb.Database.Models;
using Serilog;

namespace RoadTrip.RoadTripServices.RoadTripServices.Services
{
    public partial class QuizService
    {
        /// <inheritdoc/>
        public async Task<bool> AddQuiz(Quiz quiz)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(quiz, "null not accepted");
                var added = await _quizRepo.AddAsync(quiz);
                ArgumentNullException.ThrowIfNull(added, "failed to add quiz to Db");

                if (quiz.Questions.Count != 0)
                {
                    foreach (var question in quiz.Questions)
                    {
                        question.QuizId = added.Id;
                        await _questionRepo.AddAsync(question);
                    }
                }

                if (quiz.Vehicles.Count != 0)
                {
                    var maps = new List<QuizVehicles>();
                    foreach (var vehicle in quiz.Vehicles)
                    {
                        maps.Add(new QuizVehicles()
                        {
                            VehicleId = vehicle.Id,
                            QuizId = added.Id
                        });
                    }
                    await _quizRepo.AddRangeVehicleToQuiz(maps);
                }
            }
            catch (ArgumentNullException ex)
            {
                Log.Error("Null exception on add quiz {ex}", ex);
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("Unhandled exception on add quiz {ex}", ex);
                return false;
            }
            return true;
        }
    }
}