using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripDb.Database.Models
{
    public class QuizVehicles
    {
        public int Id { get; set; }
        public Guid QuizId { get; set; }
        public int VehicleId { get; set; }
    }
}
