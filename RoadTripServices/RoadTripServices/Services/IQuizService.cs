using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.ViewModels;
using System.Collections.Immutable;

namespace RoadTrip.RoadTripServices.RoadTripServices.Services
{
    public interface IQuizService
    {
        /// <summary>
        /// Get all quizzes for owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        Task<ICollection<Quiz>?> GetAllQuizzesForOwner(Guid? ownerId);

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
        Task<Quiz?> GetQuiz(Guid? quizId);

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

        /// <summary>
        /// Gets all open and active quizzes. These are quizzes anyone can join
        /// </summary>
        /// <returns></returns>
        Task<IImmutableList<Quiz>> GetOpenActiveQuizzes();

        /// <summary>
        /// Get an invite only quiz from the invite object
        /// </summary>
        /// <returns></returns>
        Task<Quiz> GetInviteOnlyQuiz();

        /// <summary>
        /// Activates or deactives the given quiz, deactivates all other active quizzes for this user
        /// </summary>
        /// <param name="quiz"></param>
        /// <returns></returns>
        Task<Quiz> ActivateQuiz(Quiz quiz);
        /// <summary>
        /// Setup the active quiz to track progess as an initializer. This requires sending a collection of guests to setup their progress
        /// </summary>
        /// <param name="quiz"></param>
        /// <param name="hostNameIdentifier"></param>
        /// <returns>A collection of initial quiz progresses for the guest and host</returns>
        Task<ActiveQuizProgress?> InitializeActiveQuiz(Quiz quiz, string hostNameIdentifier);
        /// <summary>
        /// Adds guests to a quiz
        /// </summary>
        /// <param name="activeQuizProgress"></param>
        /// <param name="guestAppUsers"></param>
        /// <returns></returns>
        IAsyncEnumerable<ActiveQuizProgress>? AddUsersToActiveQuiz(ActiveQuizProgress activeQuizProgress, IEnumerable<GuestAppUser> guestAppUsers);
        /// <summary>
        /// Starts the given games
        /// </summary>
        /// <param name="gamesToStart"></param>
        /// <returns>A collection of started games</returns>
        IAsyncEnumerable<ActiveQuizProgress> StartGame(IAsyncEnumerable<ActiveQuizProgress> gamesToStart);
        /// <summary>
        /// Get a view model for the active quiz data set
        /// </summary>
        /// <param name="activeQuizProgresses"></param>
        /// <returns>View model for the quiz progress</returns>
        IAsyncEnumerable<ActiveQuizProgressViewModel>? ConvertQuizProgressToViewModel(IAsyncEnumerable<ActiveQuizProgress> activeQuizProgresses);
        /// <summary>
        /// Get a single view model conversion
        /// </summary>
        /// <param name="activeQuiz"></param>
        /// <returns></returns>
        Task<ActiveQuizProgressViewModel?> ConvertQuizProgressToViewModel(ActiveQuizProgress activeQuiz);
        /// <summary>
        /// Gets the host name ID for the quiz that is active
        /// </summary>
        /// <param name="quiz"></param>
        /// <returns></returns>
        string? GetHostNameIdentifierForActiveQuiz(Quiz quiz);

    }
}