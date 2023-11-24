using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.RoadTripServices.RoadTripServices.Services;

namespace RoadTripTests.ServiceTests
{
    public class QuizServiceTests
    {
        private const string Owner1 = "ed89f407-abf0-42e3-b5d8-70c0915503eb";

        [Theory]
        [InlineData(Owner1)]
        public async Task GetAllQuizzesForOwner_OnlyReturnsQuizzesForOwner(string ownerIdString)
        {
            Guid ownerId = Guid.Parse(ownerIdString);
            IQuizService quizService = new FakeQuizService();
            IEnumerable<Quiz> foundQuizzes = await quizService.GetAllQuizzesForOwner(ownerId);
            IEnumerable<Quiz> expected =
                [
                new Quiz
                {
                    Title = "Title1",
                    Description = "Description1",
                    MaximumAnswers = 10,
                    MinimumAnswers = 5,
                    OwnerId = ownerId,
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
                        OwnerId = ownerId,
                        TotalDistance = 105,
                        TotalScore = 105,
                        Active = false,
                        Id = Guid.Parse("57c21187-224c-4c51-8efb-f830c00de0bc")
                    },
                ];
            foundQuizzes.Should().BeEquivalentTo(expected);
        }
    }
}
