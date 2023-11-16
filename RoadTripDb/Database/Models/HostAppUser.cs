using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripDb.Database.Models
{
    public class HostAppUser : RoadTripAppUser
    {
        [Key]
        public Guid RoadTripUserId { get; set; }
        public virtual ICollection<Room> OpenRooms { get; set; }
    }
}
