using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using RoadTrip.Data;
using Serilog;

namespace RoadTrip.Hubs.Security
{
    public static class UserCheck
    {
        /// <summary>
        /// Checks if this user is authenticated to access this hub
        /// </summary>
        /// <param name="hub">Current signalR hub to authenticate</param>
        /// <param name="identityDbContext">TODO: Replace with repo abstraction</param>
        /// <param name="userManager">To get the user from the store</param>
        /// <param name="userId">The expected user ID that the data should belong to</param>
        /// <returns>Whether user is allowed access or not</returns>
        public static async Task<bool> CheckUserIsAuthenticated(this Hub hub, RoadTripIdentityDbContext identityDbContext, UserManager<RoadTripUser> userManager, Guid userId)
        {
            // TODO: DO authentication better, as this is a bad way to do it
            // SignalR shouldn't use HTTP Header for auth, as it can't confirm it has been updated correctly in its connection
            // This opens the risk to expired or falsified tokens
            // But it is better than nothing
            var httpContext = hub.Context.GetHttpContext();
            var authHeaders = httpContext?.Request.Headers.Authorization;
            if (!authHeaders.HasValue)
            {
                Log.Error("User not authenticated {id}. Auth headers do not exist", userId);
                return false;
            }

            var token = Array.Find(authHeaders.Value.ToArray(), token => !string.IsNullOrWhiteSpace(token) && token.Contains("Bearer"));
            if (string.IsNullOrWhiteSpace(token))
            {
                Log.Error("User not authenticated {id}. Token not found", userId);
                return false;
            }
            var cleanToken = token.Replace("Bearer", "").Trim();

            // TODO: Make this a repo
            var tokenUser = identityDbContext.UserTokens.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.Value) && x.Value.Equals(cleanToken));

            if (tokenUser == null)
            {
                Log.Error("User not authenticated {id}. User not found via token in identity DB", userId);
                return false;
            }

            var user = await userManager.FindByIdAsync(tokenUser.UserId);

            if (user == null || user.RoadTripId != userId)
            {
                Log.Error("User not authenticated {id}. User doesn't match expected owner for this content", userId);
                return false;
            }

            Log.Information("User authecticated");
            return true;
        }
    }
}
