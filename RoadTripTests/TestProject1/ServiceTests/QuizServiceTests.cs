using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.RoadTripServices.RoadTripServices.Services;

namespace RoadTripTests.ServiceTests
{
    public class QuizServiceTests
    {
        private const string Owner1 = "ed89f407-abf0-42e3-b5d8-70c0915503eb";
        private const string Owner2 = "fe89f407-abf0-42e3-b5d8-70c0915503eb";

        [Theory]
        [InlineData(Owner1)]
        [InlineData(Owner2)]
        public async Task GetAllQuizzesForOwner_OnlyReturnsQuizzesForOwner(string ownerIdString)
        {
            Guid ownerId = Guid.Parse(ownerIdString);
            IQuizService quizService = new FakeQuizService();
            IEnumerable<Quiz> foundQuizzes = await quizService.GetAllQuizzesForOwner(ownerId);
            var foundOwnerIds = foundQuizzes.Select(f => f.OwnerId);

            foundOwnerIds.Should().Contain(ownerId);
        }

        [Theory]
        [InlineData(Owner1)]
        public async Task GetActiveQuizzesForOwner_OnlyGetsActiveForOwner(string ownerIdString)
        {
            Guid ownerId = Guid.Parse(ownerIdString);
            IQuizService quizService = new FakeQuizService();
            var foundQuizzes = await quizService.GetActiveQuizzesForOwner(ownerId);

            foundQuizzes.Should().OnlyContain(x => x.Active);
            foundQuizzes.Should().OnlyContain(x=>x.OwnerId.Equals(ownerId));
        }
    }
}
