using Microsoft.AspNetCore.SignalR;
using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.RoadTripDb.Repos;
using RoadTrip.RoadTripServices.RoadTripServices.Services;
using RoadTrip.ViewModels;
using Serilog;

namespace RoadTrip.Hubs
{
    public class QuizHub(IUserService userService, IQuizService quizService, IFuelTypeRepo fuelTypeRepo, IVehicleRepo vehicleRepo) : Hub
    {
        private readonly IUserService _userService = userService;
        private readonly IQuizService _quizService = quizService;
        private readonly IVehicleRepo _vehicleRepo = vehicleRepo;
        private readonly IFuelTypeRepo _fuelTypeRepo = fuelTypeRepo;

        public async Task AddQuiz(Quiz quiz)
        {
            var success = await _quizService.AddQuiz(quiz);
            await Clients.Caller.SendAsync("AddQuizSuccess", success);
        }

        public async Task GetAllQuizzesForOwner(Guid ownerId)
        {
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

        public async Task SetInitialQuizProgress(CurrentQuizProgress currentQuizProgress)
        {
        }

        public async Task UpdateCurrentQuizProgress(CurrentQuizProgress currentQuizProgress)
        {
            await Clients.User(currentQuizProgress.OwnerNameIdentifier).SendAsync("CurrentQuizProgress", currentQuizProgress);
        }
    }
}