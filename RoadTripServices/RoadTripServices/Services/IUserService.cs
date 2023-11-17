using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.RoadTripServices.RoadTripServices.MiddlewareModels;
using RoadTrip.ViewModels;

namespace RoadTrip.RoadTripServices.RoadTripServices.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Adds a new host to the system
        /// </summary>
        /// <param name="addNewHostModel"></param>
        /// <returns></returns>
        Task AddNewHost(AddNewHostModel addNewHostModel);

        /// <summary>
        /// Updates an individual user subscription to the given new subscription parameters
        /// </summary>
        /// <param name="newSubscription"></param>
        /// <returns></returns>
        Task<IndividualSubscription> UpdateIndividualSubscription(IndividualSubscription newSubscription);
        /// <summary>
        /// Get my sub view model from data
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        Task<MySubscription> GetIndividualSubscriptionViewModel(Guid ownerId);
        /// <summary>
        /// Gets whether the host can add new quizzes based on their sub tier
        /// </summary>
        /// <param name="hostId"></param>
        /// <returns></returns>
        Task<bool> CanHostAddNewQuizIndividual(Guid hostId);
    }
}
