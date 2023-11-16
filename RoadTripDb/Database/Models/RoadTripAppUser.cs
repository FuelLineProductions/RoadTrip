using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripDb.Database.Models
{
    public class RoadTripAppUser
    {
        public string PrimaryName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string DisplayName { get; set; } = null!;

    }
}
