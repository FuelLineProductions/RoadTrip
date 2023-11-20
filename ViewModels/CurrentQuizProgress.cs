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
    }
}
