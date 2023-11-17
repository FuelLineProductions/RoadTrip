using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripServices.RoadTripServices.Services
{
    public interface IQuizService
    {
        Task<ICollection<Quiz>> GetAllQuizzesForOwner(Guid ownerId);
        Task<ICollection<Quiz>> GetActiveQuizzesForOwner(Guid ownerId);
        Task<Quiz> GetQuiz(Guid quizId);
        Task<bool> AddQuiz(Quiz quiz);
        Task<bool> UpdateQuiz(Quiz quiz);
        Task RemoveQuestionFromQuiz(Question question);
        Task RemoveVehicleFromQuiz(Quiz quiz, int vehicleId);
    }
}
