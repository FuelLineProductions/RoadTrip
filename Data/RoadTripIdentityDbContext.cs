using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RoadTrip.Data
{
    public class RoadTripIdentityDbContext(DbContextOptions<RoadTripIdentityDbContext> options) : IdentityDbContext<RoadTripUser>(options)
    {
    }
}
