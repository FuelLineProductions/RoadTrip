﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripDb.Database.Models
{
    public class Question
    {
        public int Id { get; set; }
        public Guid QuizId { get; set; }
        public string QuestionTitle { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!;
        public int FuelRewardCorrectAnswer { get; set; }
        public int FuelRewardIncorrectAnswer { get; set; }
        public int PointsReward { get; set; }
    }
}
