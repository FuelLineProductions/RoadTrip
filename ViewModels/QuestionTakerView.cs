using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.ViewModels
{
    public class QuestionTakerView : Question
    {
        public QuestionTakerView(Question question)
        {
            Id = question.Id;
            QuizId = question.QuizId;
            QuestionTitle = question.QuestionTitle;
            CorrectAnswer = question.CorrectAnswer;
            FuelRewardCorrectAnswer = question.FuelRewardCorrectAnswer;
            FuelRewardIncorrectAnswer = question.FuelRewardIncorrectAnswer;
            PointsReward = question.PointsReward;
        }

        public bool Show { get; set; }
    }
}