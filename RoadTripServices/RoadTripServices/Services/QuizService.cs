using Microsoft.Extensions.Hosting;
using RoadTrip.Components.Pages;
using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.RoadTripDb.Repos;
using RoadTrip.ViewModels;
using Serilog;
using System.Collections.Immutable;

namespace RoadTrip.RoadTripServices.RoadTripServices.Services
{
    public class QuizService(IQuizRepo quizRepo, IQuestionRepo questionRepo, IVehicleRepo vehicleRepo, IActiveQuizProgressRepo activeQuizProgressRepo, IHostAppUserRepo hostAppUserRepo, IGuestAppUserRepo guestAppUserRepo) : IQuizService
    {
        private readonly IQuizRepo _quizRepo = quizRepo;
        private readonly IQuestionRepo _questionRepo = questionRepo;
        private readonly IVehicleRepo _vehicleRepo = vehicleRepo;
        private readonly IActiveQuizProgressRepo _activeQuizProgressRepo = activeQuizProgressRepo;
        private readonly IHostAppUserRepo _hostAppUserRepo = hostAppUserRepo;
        private readonly IGuestAppUserRepo _guestAppUserRepo = guestAppUserRepo;


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

        /// <inheritdoc/>
        public async Task<IImmutableList<Quiz>> GetOpenActiveQuizzes()
        {
            // TODO: Add functionality to differenciate between open to anyone, and invite only quizzes
            return [.. _quizRepo.GetQueryable().Where(x => x.Active)];
        }

        /// <inheritdoc/>
        public async Task<Quiz> GetInviteOnlyQuiz()
        {
            // TODO Make and implement a way of sending an invite only quiz option
            throw new NotImplementedException();
        }

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
        public string? GetHostNameIdentifierForActiveQuiz(Quiz quiz)
        {
            var active = _activeQuizProgressRepo.GetQueryable().FirstOrDefault(x => x.QuizId.Equals(quiz.Id) && x.HostId.Equals(quiz.OwnerId) && !x.QuizEnded.HasValue);
            return active?.HostNameIdentifier;
        }
    }
}