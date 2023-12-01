using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.ViewModels;
using Serilog;

namespace RoadTrip.RoadTripServices.RoadTripServices.Services
{
    public partial class QuizService
    {
        /// <inheritdoc/>
        public async Task<Quiz> ActivateQuiz(Quiz quiz)
        {
            // If other quizes are active, deactivate them
            var quizzes = _quizRepo.GetManyForOwner(quiz.OwnerId);
            if (quizzes != null)
            {
                foreach (var activeQuiz in quizzes.Where(x => x.Active))
                {
                    activeQuiz.Active = false;
                    await _quizRepo.UpdateAsync(activeQuiz);
                }
            }

            // Attempting to avoid tracking error
            var dbQuiz = await _quizRepo.Get(quiz.Id) ?? quiz;
            dbQuiz.Active = !dbQuiz.Active;
            return await _quizRepo.UpdateAsync(dbQuiz);
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<ActiveQuizProgress>? AddUsersToActiveQuiz(ActiveQuizProgress activeQuizProgress, IEnumerable<GuestAppUser> guestAppUsers)
        {
            foreach (var user in guestAppUsers)
            {
                // Make a new db entry value for each user
                var newQuiz = new ActiveQuizProgress()
                {
                    HostId = activeQuizProgress.HostId,
                    QuizId = activeQuizProgress.QuizId,
                    HostNameIdentifier = activeQuizProgress.HostNameIdentifier,
                    GuestId = user.GuestId,
                    FuelGuess = activeQuizProgress.FuelGuess,
                    CompletedFuel = activeQuizProgress.CompletedFuel,
                    CompletedScore = activeQuizProgress.CompletedScore,
                    CurrentAnswer = activeQuizProgress.CurrentAnswer,
                    CurrentFuel = activeQuizProgress.CurrentFuel,
                    CurrentQuestionId = activeQuizProgress.CurrentQuestionId,
                    CurrentScore = activeQuizProgress.CurrentScore,
                    QuizEnded = activeQuizProgress.QuizEnded,
                    QuizStarted = activeQuizProgress.QuizStarted,
                    StartedQuestion = activeQuizProgress.StartedQuestion,
                    StartScore = activeQuizProgress.StartScore
                };
                yield return await _activeQuizProgressRepo.AddAsync(newQuiz);
            }
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<ActiveQuizProgressViewModel>? ConvertQuizProgressToViewModel(IAsyncEnumerable<ActiveQuizProgress> activeQuizProgresses)
        {
            var activeQuizzes = activeQuizProgresses.ToBlockingEnumerable();
            var owners = activeQuizzes.Select(x => x.HostId).Distinct();
            // if there are more than one owner then there is an issue
            if (owners.Count() > 1)
            {
                Log.Error("Too many hosts in this active game, cannot continue");
                throw new InvalidDataException("Too many hosts in this active game, cannot continue"); ;
            }

            var ownerId = owners.First();
            var host = await _hostAppUserRepo.GetHostAppUser(ownerId);

            var quizzes = activeQuizzes.Select(x => x.QuizId).Distinct();
            if (quizzes.Count() > 1)
            {
                Log.Error("Too many quizzes in this active game, cannot continue");
                throw new InvalidDataException("Too many quizzes in this active game, cannot continue");
            }

            var quizId = quizzes.First();
            var quiz = await _quizRepo.Get(quizId);

            var quizViewModels = new List<ActiveQuizProgressViewModel>();
            foreach (var activeQuiz in activeQuizzes)
            {
                GuestAppUser? guest = null;

                if (activeQuiz.GuestId.HasValue)
                {
                    guest = await _guestAppUserRepo.Get(activeQuiz.GuestId.Value);
                }

                Question? currentQuestion = null;
                if (activeQuiz.CurrentQuestionId.HasValue)
                {
                    currentQuestion = await _questionRepo.Get(activeQuiz.CurrentQuestionId.Value);
                }

                yield return new ActiveQuizProgressViewModel(activeQuiz)
                {
                    Quiz = quiz,
                    HostAppUser = host,
                    GuestAppUser = guest,
                    CurrentQuestion = currentQuestion
                };
            }
        }

        /// <inheritdoc />
        public async Task<ActiveQuizProgressViewModel?> ConvertQuizProgressToViewModel(ActiveQuizProgress activeQuiz)
        {
            var host = await _hostAppUserRepo.GetHostAppUser(activeQuiz.HostId);
            var quiz = await _quizRepo.Get(activeQuiz.QuizId);

            GuestAppUser? guest = null;

            if (activeQuiz.GuestId.HasValue)
            {
                guest = await _guestAppUserRepo.Get(activeQuiz.GuestId.Value);
            }

            Question? currentQuestion = null;
            if (activeQuiz.CurrentQuestionId.HasValue)
            {
                currentQuestion = await _questionRepo.Get(activeQuiz.CurrentQuestionId.Value);
            }

            return new ActiveQuizProgressViewModel(activeQuiz)
            {
                Quiz = quiz,
                HostAppUser = host,
                GuestAppUser = guest,
                CurrentQuestion = currentQuestion
            };
        }

        /// <inheritdoc />
        public string? GetHostNameIdentifierForActiveQuiz(Quiz quiz)
        {
            var active = _activeQuizProgressRepo.GetQueryable().FirstOrDefault(x => x.QuizId.Equals(quiz.Id) && x.HostId.Equals(quiz.OwnerId) && !x.QuizEnded.HasValue);
            return active?.HostNameIdentifier;
        }

        /// <inheritdoc />
        public async Task<ActiveQuizProgress?> InitializeActiveQuiz(Quiz quiz, string hostNameIdentifier)
        {
            if (!quiz.Active)
            {
                Log.Warning("Failed to setup quiz {id} as it is not active, please activate the guess first", quiz.Id);
                return null;
            }

            ActiveQuizProgress newActiveQuiz = new()
            {
                HostId = quiz.OwnerId,
                QuizId = quiz.Id,
                HostNameIdentifier = hostNameIdentifier,
            };

            return await _activeQuizProgressRepo.AddAsync(newActiveQuiz);
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<ActiveQuizProgress> StartGame(IAsyncEnumerable<ActiveQuizProgress> gamesToStart)
        {
            var now = DateTime.UtcNow;
            await foreach (var game in gamesToStart)
            {
                game.QuizStarted = now;
                game.CurrentQuestionId = _questionRepo.GetQueryable().Where(x => x.QuizId.Equals(game.QuizId)).FirstOrDefault()?.Id;
                yield return await _activeQuizProgressRepo.UpdateAsync(game);
            }
        }
    }
}