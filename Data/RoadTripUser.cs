using Microsoft.AspNetCore.Identity;

namespace RoadTrip.Data
{
    // Add profile data for application users by adding properties to the RoadTripUser class
    public class RoadTripUser : IdentityUser
    {
        public Guid RoadTripId { get; set; } = Guid.NewGuid();
        [PersonalData]
        public string PrimaryName { get; set; } = null!;
        [PersonalData]
        public string Surname { get; set; } = null!;
        [PersonalData]
        public string DisplayName { get; set; } = null!;
    }

}
