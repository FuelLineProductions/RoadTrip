using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripDb.Database.Models
{
    public class SubscriptionTier
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double YearlyCostGbp { get; set; }
        public double DiscountGbp { get; set; }
        public double MonthlyCostGbp { get; set; }
        public int? MaxGuests { get; set; }
        public int? MaxRooms { get; set; }
        public int? MaxGameSaves { get; set; }
        public int? MaxHosts { get; set; }
    }
}
