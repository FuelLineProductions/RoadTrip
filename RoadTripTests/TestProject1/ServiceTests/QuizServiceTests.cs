using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.RoadTripServices.RoadTripServices.Services;

namespace RoadTripTests.ServiceTests
{
    public class QuizServiceTests
    {
        private const string Owner1 = "ed89f407-abf0-42e3-b5d8-70c0915503eb";
        private const string Owner2 = "fe89f407-abf0-42e3-b5d8-70c0915503eb";
        private const string Quiz1Id = "c111a97f-17f9-4605-8d21-fc5364f4f1c8";
        private const string Quiz2Id = "57c21187-224c-4c51-8efb-f830c00de0bc";
        private const string Quiz3Id = "68c21187-224c-4c51-8efb-f830c00de0bc";

        [Theory]
        [InlineData(Owner1)]
        public async Task GetActiveQuizzesForOwner_OnlyGetsActiveForOwner(string ownerIdString)
        {
            Guid ownerId = Guid.Parse(ownerIdString);
            IQuizService quizService = new FakeQuizService();
            var foundQuizzes = await quizService.GetActiveQuizzesForOwner(ownerId);

            foundQuizzes.Should().OnlyContain(x => x.Active);
            foundQuizzes.Should().OnlyContain(x => x.OwnerId.Equals(ownerId));
        }

        [Fact]
        public async Task GetAllQuizzesForOwner_EmptyGuidReturnsNull()
        {
            var emptyGuid = Guid.Empty;
            IQuizService quizService = new FakeQuizService();
            var quizzes = await quizService.GetAllQuizzesForOwner(emptyGuid);
            quizzes.Should().BeNull();
        }

        [Fact]
        public async Task GetAllQuizzesForOwner_NullGuidReturnsNull()
        {
            Guid? nullGuid = null;
            IQuizService quizService = new FakeQuizService();
            var quizzes = await quizService.GetAllQuizzesForOwner(nullGuid);
            quizzes.Should().BeNull();
        }

        [Theory]
        [InlineData(Owner1)]
        [InlineData(Owner2)]
        public async Task GetAllQuizzesForOwner_OnlyReturnsQuizzesForOwner(string ownerIdString)
        {
            Guid ownerId = Guid.Parse(ownerIdString);
            IQuizService quizService = new FakeQuizService();
            var foundQuizzes = await quizService.GetAllQuizzesForOwner(ownerId);
            var foundOwnerIds = foundQuizzes?.Select(f => f.OwnerId);

            foundOwnerIds.Should().NotBeNullOrEmpty();
            foundOwnerIds.Should().Contain(ownerId);
        }

        [Theory]
        [InlineData(Quiz1Id)]
        [InlineData(Quiz2Id)]
        [InlineData(Quiz3Id)]
        public async Task GetQuizById_GetsOnlyQuizWithThatId(string quiz1Id)
        {
            Guid quizId = Guid.Parse(quiz1Id);
            IQuizService quizService = new FakeQuizService();
            var quiz = await quizService.GetQuiz(quizId);
            quiz.Should().NotBeNull();
            quiz.Id.Should().Be(quizId);
        }
    }
}