using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripServices.RoadTripServices.Services
{
    public interface IQuizService
    {
        /// <summary>
        /// Get all quizzes for owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        Task<ICollection<Quiz>> GetAllQuizzesForOwner(Guid ownerId);
        /// <summary>
        /// Get active quizzes for owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        Task<ICollection<Quiz>> GetActiveQuizzesForOwner(Guid ownerId);
        /// <summary>
        /// Get individual quiz by ID
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        Task<Quiz> GetQuiz(Guid quizId);
        /// <summary>
        /// Add a new quiz
        /// </summary>
        /// <param name="quiz"></param>
        /// <returns></returns>
        Task<bool> AddQuiz(Quiz quiz);
        /// <summary>
        /// Update quiz
        /// </summary>
        /// <param name="quiz"></param>
        /// <returns></returns>
        Task<bool> UpdateQuiz(Quiz quiz);
        /// <summary>
        /// Remove question from quiz
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        Task RemoveQuestionFromQuiz(Question question);
        /// <summary>
        /// Remove a vehicle from quiz
        /// </summary>
        /// <param name="quiz"></param>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        Task RemoveVehicleFromQuiz(Quiz quiz, int vehicleId);
    }
}
