using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripDb.Database.Models
{
    // TODO: Implement later
    public class QuestionMultipleChoice : Question
    {
        public ICollection<string> PotentialAnswers { get; set; } = null!;
    }
}
