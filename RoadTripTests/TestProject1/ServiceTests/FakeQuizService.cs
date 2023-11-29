using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.RoadTripServices.RoadTripServices.Services;
using RoadTrip.ViewModels;
using System.Collections.Immutable;

namespace RoadTripTests.ServiceTests
{
    public class FakeQuizService : IQuizService
    {
        private readonly ImmutableList<Quiz> FakeQuizzes = [
                new Quiz
                {
                    Title = "Title1",
                    Description = "Description1",
                    MaximumAnswers = 10,
                    MinimumAnswers = 5,
                    OwnerId = Guid.Parse("ed89f407-abf0-42e3-b5d8-70c0915503eb"),
                    TotalDistance = 100,
                    TotalScore = 100,
                    Active = true,
                    Id = Guid.Parse("c111a97f-17f9-4605-8d21-fc5364f4f1c8")
                },
            new Quiz
            {
                Title = "Title2",
                Description = "Description2",
                MaximumAnswers = 15,
                MinimumAnswers = 2,
                OwnerId = Guid.Parse("ed89f407-abf0-42e3-b5d8-70c0915503eb"),
                TotalDistance = 105,
                TotalScore = 105,
                Active = false,
                Id = Guid.Parse("57c21187-224c-4c51-8efb-f830c00de0bc")
            },
            new Quiz
            {
                Title = "Title3",
                Description = "Description3",
                MaximumAnswers = 15,
                MinimumAnswers = 2,
                OwnerId = Guid.Parse("fe89f407-abf0-42e3-b5d8-70c0915503eb"),
                TotalDistance = 105,
                TotalScore = 105,
                Active = false,
                Id = Guid.Parse("68c21187-224c-4c51-8efb-f830c00de0bc")
            },
        ];

        /// <inheritdoc/>
        public async Task<ICollection<Quiz>?> GetAllQuizzesForOwner(Guid? ownerId)
        {
            if (!ownerId.HasValue || ownerId == Guid.Empty)
            {
                return null;
            }
            return FakeQuizzes.Where(q => q.OwnerId.Equals(ownerId.Value)).ToList();
        }

        /// <inheritdoc/>
        public async Task<ICollection<Quiz>> GetActiveQuizzesForOwner(Guid ownerId)
        {
            return FakeQuizzes.Where(x => x.Active && x.OwnerId.Equals(ownerId)).ToList();
        }

        /// <inheritdoc/>
        public async Task<Quiz?> GetQuiz(Guid? quizId)
        {
            if (!quizId.HasValue || quizId == Guid.Empty)
            {
                return null;
            }
            return FakeQuizzes.FirstOrDefault(x => x.Id.Equals(quizId)) ?? new();
        }

        /// <inheritdoc/>
        public async Task<bool> AddQuiz(Quiz quiz)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateQuiz(Quiz quiz)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task RemoveQuestionFromQuiz(Question question)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task RemoveVehicleFromQuiz(Quiz quiz, int vehicleId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IImmutableList<Quiz>> GetOpenActiveQuizzes()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<Quiz> GetInviteOnlyQuiz()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<Quiz> ActivateQuiz(Quiz quiz)
        {
            throw new NotImplementedException();
        }


        IAsyncEnumerable<ActiveQuizProgress>? IQuizService.InitializeActiveQuiz(Quiz quiz, string hostNameIdentifier, IEnumerable<GuestAppUser> guestAppUser)
        {
            throw new NotImplementedException();
        }

        IAsyncEnumerable<ActiveQuizProgress> IQuizService.StartGame(IAsyncEnumerable<ActiveQuizProgress> gamesToStart)
        {
            throw new NotImplementedException();
        }

        IAsyncEnumerable<ActiveQuizProgressViewModel>? IQuizService.ConvertQuizProgressToViewModel(IAsyncEnumerable<ActiveQuizProgress> activeQuizProgresses)
        {
            throw new NotImplementedException();
        }

        string? IQuizService.GetHostNameIdentifierForActiveQuiz(Quiz quiz)
        {
            throw new NotImplementedException();
        }
    }
}