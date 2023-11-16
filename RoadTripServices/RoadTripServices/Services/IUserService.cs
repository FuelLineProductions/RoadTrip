using RoadTripDb.Database.Models;
using RoadTripServices.MiddlewareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripServices.Services
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
    }
}
