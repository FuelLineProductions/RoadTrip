﻿using Microsoft.AspNetCore.SignalR;
using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.RoadTripDb.Repos;
using RoadTrip.RoadTripServices.RoadTripServices.Services;

namespace RoadTrip.Hubs
{
    public class QuizHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IQuizService _quizService;
        private readonly IVehicleRepo _vehicleRepo;
        private readonly IFuelTypeRepo _fuelTypeRepo;

        public QuizHub(IUserService userService, IQuizService quizService, IFuelTypeRepo fuelTypeRepo, IVehicleRepo vehicleRepo)
        {
            _userService = userService;
            _quizService = quizService;
            _vehicleRepo = vehicleRepo;
            _fuelTypeRepo = fuelTypeRepo;
        }       

        public async Task AddQuiz(Quiz quiz)
        {            
            var success = await _quizService.AddQuiz(quiz);
            await Clients.Caller.SendAsync("AddQuizSuccess", success);
        }

        public async Task GetAllQuizzesForOwner(Guid ownerId)
        {
            var quizzes = await _quizService.GetAllQuizzesForOwner(ownerId);
            await Clients.Caller.SendAsync("AllQuizzesForOwner", quizzes.ToList());
        }

        public async Task CanHostAddNewQuizIndividual(Guid id)
        {
            var canAdd = await _userService.CanHostAddNewQuizIndividual(id);
            await Clients.Caller.SendAsync("CanHostAddNewQuizIndividualResult", canAdd);
        }

        public async Task GetAllVehicles()
        {
            var vehicles = _vehicleRepo.GetAll().ToList();
            foreach(var vehicle in vehicles)
            {
                vehicle.FuelType = await _fuelTypeRepo.Get(vehicle.FuelTypeId);
            }
            await Clients.Caller.SendAsync("AllVehicles", vehicles);
        }
    }
}