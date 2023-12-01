using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using RoadTrip.Data;
using RoadTrip.Hubs.Security;
using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.RoadTripDb.Repos;
using RoadTrip.RoadTripServices.RoadTripServices.Services;
using RoadTrip.ViewModels;
using Serilog;

namespace RoadTrip.Hubs
{
    public class QuizHub(IUserService userService, IQuizService quizService, IFuelTypeRepo fuelTypeRepo, IVehicleRepo vehicleRepo, IGuestAppUserRepo guestAppUserRepo, RoadTripIdentityDbContext idenitityDbContext, UserManager<RoadTripUser> userManager, IGuestQuizJoinRepo guestQuizJoinRepo) : Hub
    {
        private readonly IUserService _userService = userService;
        private readonly IQuizService _quizService = quizService;
        private readonly IVehicleRepo _vehicleRepo = vehicleRepo;
        private readonly IFuelTypeRepo _fuelTypeRepo = fuelTypeRepo;
        private readonly IGuestAppUserRepo _guestAppUserRepo = guestAppUserRepo;
        // TODO Make a repo for this instead
        private readonly RoadTripIdentityDbContext _identityDbContext = idenitityDbContext;
        private readonly UserManager<RoadTripUser> _userManager = userManager;
        private readonly IGuestQuizJoinRepo _guestQuizJoinRepo = guestQuizJoinRepo;

        public async Task AddQuiz(Quiz quiz)
        {
            var success = await _quizService.AddQuiz(quiz);
            await Clients.Caller.SendAsync("AddQuizSuccess", success);
        }

        public async Task GetAllQuizzesForOwner(Guid ownerId)
        {
            var isAuthenticated = await this.CheckUserIsAuthenticated(_identityDbContext, _userManager, ownerId);
            if (!isAuthenticated)
            {
                Log.Error("User is not authenticated");
                return;
            }
            var quizzes = await _quizService.GetAllQuizzesForOwner(ownerId);
            Log.Information("Got quizzes for owner {quizzes}, {ownerId}", string.Join(", ", quizzes.Select(x => x.Title)), ownerId);
            await Clients.Caller.SendAsync("AllQuizzesForOwner", quizzes.Count != 0 ? quizzes.ToList() : []);
        }

        public async Task CanHostAddNewQuizIndividual(Guid id)
        {
            var canAdd = await _userService.CanHostAddNewQuizIndividual(id);
            await Clients.Caller.SendAsync("CanHostAddNewQuizIndividualResult", canAdd);
        }

        public async Task GetAllVehicles()
        {
            var vehicles = _vehicleRepo.GetAll().ToList();
            foreach (var vehicle in vehicles)
            {
                vehicle.FuelType = await _fuelTypeRepo.Get(vehicle.FuelTypeId);
            }
            await Clients.Caller.SendAsync("AllVehicles", vehicles);
        }

        public async Task SetInitialQuizProgress(Quiz quiz, string hostName, IEnumerable<GuestAppUser> users, bool startGame)
        {
            var intializedQuiz = await _quizService.InitializeActiveQuiz(quiz, hostName);
            if (intializedQuiz != null)
            {
                if (!startGame)
                {
                    Log.Information("Quiz initalised, but start not requested");
                    var converted = _quizService.ConvertQuizProgressToViewModel(intializedQuiz);
                    await Clients.Caller.SendAsync("InitializedQuizzes", converted);
                }
                else
                {
                    Log.Information("Quiz initalised, Game started");
                    var addedUsers = _quizService.AddUsersToActiveQuiz(intializedQuiz, users);
                    if (addedUsers != null)
                    {
                        var started = _quizService.StartGame(addedUsers);
                        var converted = _quizService.ConvertQuizProgressToViewModel(started);
                        await Clients.Caller.SendAsync("InitializedQuizzes", converted.ToBlockingEnumerable());
                    }
                }
            }
        }

        public async Task ActivateQuiz(Quiz quiz)
        {
            var activated = await _quizService.ActivateQuiz(quiz);
            if (activated != null)
            {
                await Clients.Caller.SendAsync("ActivatedQuiz", activated);
            }
        }

        public async Task RequestToJoinQuiz(Quiz quizToJoin, GuestAppUser guestAppUser)
        {
            var newGuest = await _guestQuizJoinRepo.AddAsync(new GuestRequestJoinQuiz
            {
                QuizId = quizToJoin.Id,
                GuestId = guestAppUser.GuestId,
                RequestedAt = DateTime.UtcNow,
            });       

            await Clients.All.SendAsync("NewGuests", newGuest, guestAppUser);
        }

        public async Task OpenQuizzes()
        {
            var quizzes = await _quizService.GetOpenActiveQuizzes();
            await Clients.Caller.SendAsync("JoinableQuizzes", quizzes);
        }

        public async Task SetupGuest(GuestAppUser guest)
        {
            var createdGuest = await _guestAppUserRepo.AddAsync(guest);
            await Clients.Caller.SendAsync("CreatedGuest", createdGuest);
        }

        //public async Task UpdateCurrentQuizProgress(CurrentQuizProgress currentQuizProgress)
        //{
        //    await Clients.User(currentQuizProgress.OwnerNameIdentifier).SendAsync("CurrentQuizProgress", currentQuizProgress);
        //}
    }
}