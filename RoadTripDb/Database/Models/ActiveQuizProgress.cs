using System.ComponentModel.DataAnnotations;

namespace RoadTrip.RoadTripDb.Database.Models
{
    public class ActiveQuizProgress
    {
        [Key]
        public int Id { get; set; }

        public Guid HostId { get; set; }

        /// <summary>
        /// <para>Needs to be from the owner's name principal from identity</para>
        /// <para>
        /// By default, SignalR uses the ClaimTypes.NameIdentifier from the ClaimsPrincipal
        /// associated with the connection as the user identifier. To customize this behavior, see
        /// Use claims to customize identity handling.
        /// </para>
        /// </summary>
        public string? HostNameIdentifier { get; set; }

        public Guid? GuestId { get; set; }
        public Guid QuizId { get; set; }
        public int? CurrentQuestionId { get; set; }
        public string? CurrentAnswer { get; set; }
        public int? CurrentScore { get; set; }
        public int? CurrentFuel { get; set; }
        public DateTime? StartedQuestion { get; set; }
        public int? FuelGuess { get; set; }
        public int? StartScore { get; set; }
        public int? CompletedScore { get; set; }
        public int? CompletedFuel { get; set; }
        public DateTime? QuizStarted { get; set; }
        public DateTime? QuizEnded { get; set; }
    }
}