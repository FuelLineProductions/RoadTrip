using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripDb.Database.Models
{
    public class GuestAppUser : RoadTripAppUser
    {
        [Key]
        public Guid GuestId { get; set; }
        public Guid HostId { get; set; }
        public Guid? ActiveRoomId { get; set; }
        public ICollection<Guid> RoomInvites { get; set; }
    }
}
