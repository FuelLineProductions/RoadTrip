using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoadTripDb.Database.Models;

namespace RoadTripDb.Database
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
        public DbSet<QuizVehicles> QuizVechicles { get; set; } = null!;
    }
}
