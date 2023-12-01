using RoadTrip.RoadTripDb.Repos;

namespace RoadTrip.RoadTripServices.RoadTripServices.Services
{
    public partial class QuizService(IQuizRepo quizRepo, IQuestionRepo questionRepo, IVehicleRepo vehicleRepo, IActiveQuizProgressRepo activeQuizProgressRepo, IHostAppUserRepo hostAppUserRepo, IGuestAppUserRepo guestAppUserRepo) : IQuizService
    {
        private readonly IQuizRepo _quizRepo = quizRepo;
        private readonly IQuestionRepo _questionRepo = questionRepo;
        private readonly IVehicleRepo _vehicleRepo = vehicleRepo;
        private readonly IActiveQuizProgressRepo _activeQuizProgressRepo = activeQuizProgressRepo;
        private readonly IHostAppUserRepo _hostAppUserRepo = hostAppUserRepo;
        private readonly IGuestAppUserRepo _guestAppUserRepo = guestAppUserRepo;
    }
}