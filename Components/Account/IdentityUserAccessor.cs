using Microsoft.AspNetCore.Identity;
using RoadTrip.Data;

namespace RoadTrip.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<RoadTripUser> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<RoadTripUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}