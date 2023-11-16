using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripDb.Database.Models
{
    // TODO: Implement later
    public class QuestionMaths : Question
    {
        public MathsOperatorsAllowed AllowedOperators { get; set; } = null!;
        public double MathsAnswer { get; set; }
    }
}
