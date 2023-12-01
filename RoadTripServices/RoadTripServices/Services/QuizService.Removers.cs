using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripServices.RoadTripServices.Services
{
    public partial class QuizService
    {
        /// <inheritdoc/>
        public async Task RemoveQuestionFromQuiz(Question question)
        {
            await _questionRepo.RemoveAsync(question);
        }

        /// <inheritdoc/>
        public async Task RemoveVehicleFromQuiz(Quiz quiz, int vehicleId)
        {
            await _quizRepo.RemoveVehicleFromQuiz(quiz.Id, vehicleId);
        }
    }
}