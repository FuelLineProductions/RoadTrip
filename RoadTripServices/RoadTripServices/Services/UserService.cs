﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadTripDb.Database.Models;
using RoadTripDb.Repos;
using RoadTripServices.MiddlewareModels;

namespace RoadTripServices.Services
{
    public class UserService : IUserService
    {
        private readonly IHostAppUserRepo _hostUserRepo;
        private readonly IIndividualSubscriptionRepo _individualSubscriptionRepo;
        private readonly IGroupSubscriptionRepo _groupSubscriptionRepo;

        public UserService(IHostAppUserRepo hostUserRepo, IIndividualSubscriptionRepo individualSubscriptionRepo, IGroupSubscriptionRepo groupSubscriptionRepo)
        {
            _hostUserRepo = hostUserRepo;
            _individualSubscriptionRepo = individualSubscriptionRepo;
            _groupSubscriptionRepo = groupSubscriptionRepo;
        }

        /// <inheritdoc />
        public async Task AddNewHost(AddNewHostModel addNewHostModel)
        {
            ArgumentNullException.ThrowIfNull(addNewHostModel, "No parameters passed to make a new host");

            var newHost = await _hostUserRepo.AddAsync(addNewHostModel.HostAppUser);

            ArgumentNullException.ThrowIfNull(newHost, "new host was unable to be created");

            DateTime? expiry = null;

            // if subscription is not free
            // todo: make this not magic 
            if (addNewHostModel.SubscriptionTier.Name.ToLower() != "free")
            {
                expiry = addNewHostModel.Yearly ? DateTime.UtcNow.AddYears(1) : DateTime.UtcNow.AddMonths(1);
            }

            var individualSubscription = new IndividualSubscription()
            {
                ActiveSubscriptionTier = addNewHostModel.SubscriptionTier.Id,
                IndividualId = Guid.NewGuid(),
                Owner = newHost.RoadTripUserId,
                SubscriptionExpiry = expiry
            };

            var newSub = await _individualSubscriptionRepo.AddAsync(individualSubscription);
            // if failed to assign a personal subscription
            ArgumentNullException.ThrowIfNull(newSub, "unable to add subscription to new host");

            if (addNewHostModel.GroupId.HasValue)
            {
                var group = await _groupSubscriptionRepo.Get(addNewHostModel.GroupId.Value);
                ArgumentNullException.ThrowIfNull(group, "Could not add to group, as it could not be found");
                group.HostsInSubscription.Add(newHost.RoadTripUserId);
                var updated = await _groupSubscriptionRepo.UpdateAsync(group);
                ArgumentNullException.ThrowIfNull(updated, "Could not add to group, as the update failed");
            }
        }

        /// <inheritdoc />
        public async Task<IndividualSubscription> UpdateIndividualSubscription(IndividualSubscription newSubscription)
        {
            // validate host exists
            var host = await _hostUserRepo.GetHostAppUser(newSubscription.Owner);
            ArgumentNullException.ThrowIfNull(host, "host could not be found");
            
            var currentSub = _individualSubscriptionRepo.GetQueryable().FirstOrDefault(sub => sub.Owner.Equals(newSubscription.Owner));
            // if a current subscription does not exist
            if (currentSub == null)
            {
                // Add a new sub
                var added = await _individualSubscriptionRepo.AddAsync(newSubscription);
                ArgumentNullException.ThrowIfNull(added, "Failed to add new subscription");
                return added;
            }
            // else update the existing sub
            currentSub.ActiveSubscriptionTier = newSubscription.ActiveSubscriptionTier;
            currentSub.SubscriptionExpiry = newSubscription.SubscriptionExpiry;
            currentSub.Owner = newSubscription.Owner;
            var updated = await _individualSubscriptionRepo.UpdateAsync(currentSub);
            ArgumentNullException.ThrowIfNull(updated, "Failed to update subscription");
            return updated;
        }
    }

}
