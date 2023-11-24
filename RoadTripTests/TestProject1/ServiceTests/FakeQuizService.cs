using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.RoadTripServices.RoadTripServices.Services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripTests.ServiceTests
{
    public class FakeQuizService : IQuizService
    {
        private List<Quiz> FakeQuizzes = [
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
        ];

        /// <inheritdoc/>
        public async Task<ICollection<Quiz>> GetAllQuizzesForOwner(Guid ownerId)
        {
            return FakeQuizzes.Where(q => q.OwnerId.Equals(ownerId)).ToList();
        }

        /// <inheritdoc/>
        public async Task<ICollection<Quiz>> GetActiveQuizzesForOwner(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<Quiz> GetQuiz(Guid quizId)
        {
            throw new NotImplementedException();
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
    }
}
