using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripDb.Database.Models
{
    public class FuelType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CostPerQuestion { get; set; }
    }
}
