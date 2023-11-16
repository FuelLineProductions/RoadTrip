using Microsoft.EntityFrameworkCore;
using RoadTrip.RoadTripDb.Database.Models;

namespace RoadTrip.RoadTripDb.Database
{
    public class RoadTripDbContext(DbContextOptions<RoadTripDbContext> options) : DbContext(options)
    {
        public DbSet<HostAppUser> HostAppUsers { get; set; } = null!;
        public DbSet<GuestAppUser> GuestAppUsers { get; set; } = null!;
        public DbSet<SubscriptionTier> SubscriptionTiers { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<GroupSubscription> GroupSubscriptions { get; set; } = null!;
        public DbSet<IndividualSubscription> IndividualSubscriptions { get; set; } = null!;
        public DbSet<FuelType> FuelTypes { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Quiz> Quizzes { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<QuizVehicles> QuizVehicles { get; set; } = null!;
    }
}
