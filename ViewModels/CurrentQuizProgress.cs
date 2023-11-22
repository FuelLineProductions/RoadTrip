using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.ViewModels
{
    public class CurrentQuizProgress : GuestAppUser
    {
        public Guid OwnerId { get; set; }
        public string CurrentQuestion { get; set; } = null!;
        public int CurrentScore { get; set; }
        public int CurrentFuel { get; set; }
        public DateTime? StartedQuestion { get; set; }
        /// <summary>
        /// Needs to be from the owner's name principal from identity
        /// By default, SignalR uses the ClaimTypes.NameIdentifier from the ClaimsPrincipal associated with the connection as the user identifier. To customize this behavior, see Use claims to customize identity handling.
        /// </summary>
        public string OwnerNameIdentifier { get; set; } = null!;
    }
}
