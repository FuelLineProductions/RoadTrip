using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.ViewModels
{
    public class ActiveQuizProgressViewModel : ActiveQuizProgress
    {
        public ActiveQuizProgressViewModel(ActiveQuizProgress activeQuizProgress)
        {
            QuizId = activeQuizProgress.QuizId;
            Id = activeQuizProgress.Id;
            HostId = activeQuizProgress.HostId;
            HostNameIdentifier = activeQuizProgress.HostNameIdentifier;
            GuestId = activeQuizProgress.GuestId;
            CurrentQuestionId = activeQuizProgress.CurrentQuestionId;
            CurrentAnswer = activeQuizProgress.CurrentAnswer;
            CurrentScore = activeQuizProgress.CurrentScore;
            CurrentFuel = activeQuizProgress.CurrentFuel;
            StartedQuestion = activeQuizProgress.StartedQuestion;
            FuelGuess = activeQuizProgress.FuelGuess;
            StartScore = activeQuizProgress.StartScore;
            CompletedScore = activeQuizProgress.CompletedScore;
            CompletedFuel   = activeQuizProgress.CompletedFuel;
            QuizStarted = activeQuizProgress.QuizStarted;
            QuizEnded = activeQuizProgress.QuizEnded;

        }

        public virtual Quiz? Quiz { get; set; }
        public virtual GuestAppUser? GuestAppUser { get; set; }
        public virtual HostAppUser? HostAppUser { get; set; }
        public virtual Question? CurrentQuestion { get; set; }

    }
}
