﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoadTrip.RoadTripDb.Database;

#nullable disable

namespace RoadTrip.Migrations.RoadTripDb
{
    [DbContext(typeof(RoadTripDbContext))]
    [Migration("20231117153505_remake")]
    partial class remake
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.FuelType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CostPerQuestion")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FuelTypes");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.GroupSubscription", b =>
                {
                    b.Property<Guid>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ActiveSubscriptionTier")
                        .HasColumnType("int");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HostsInSubscription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Owner")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("SubscriptionExpiry")
                        .HasColumnType("datetime2");

                    b.HasKey("GroupId");

                    b.ToTable("GroupSubscriptions");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.GuestAppUser", b =>
                {
                    b.Property<Guid>("GuestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ActiveRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PrimaryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomInvites")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GuestId");

                    b.ToTable("GuestAppUsers");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.HostAppUser", b =>
                {
                    b.Property<Guid>("RoadTripUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoadTripUserId");

                    b.ToTable("HostAppUsers");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.IndividualSubscription", b =>
                {
                    b.Property<Guid>("IndividualId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ActiveSubscriptionTier")
                        .HasColumnType("int");

                    b.Property<Guid>("Owner")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("SubscriptionExpiry")
                        .HasColumnType("datetime2");

                    b.HasKey("IndividualId");

                    b.ToTable("IndividualSubscriptions");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FuelRewardCorrectAnswer")
                        .HasColumnType("int");

                    b.Property<int>("FuelRewardIncorrectAnswer")
                        .HasColumnType("int");

                    b.Property<int>("PointsReward")
                        .HasColumnType("int");

                    b.Property<string>("QuestionTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("QuizId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.Quiz", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaximumAnswers")
                        .HasColumnType("int");

                    b.Property<int>("MinimumAnswers")
                        .HasColumnType("int");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalDistance")
                        .HasColumnType("int");

                    b.Property<int>("TotalScore")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.QuizVehicles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("QuizId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("QuizVehicles");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.Room", b =>
                {
                    b.Property<Guid>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CurrentUserCount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("HostAppUserRoadTripUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaxUsers")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoomId");

                    b.HasIndex("HostAppUserRoadTripUserId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.SubscriptionTier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("DiscountGbp")
                        .HasColumnType("float");

                    b.Property<int?>("MaxGameSaves")
                        .HasColumnType("int");

                    b.Property<int?>("MaxGuests")
                        .HasColumnType("int");

                    b.Property<int?>("MaxHosts")
                        .HasColumnType("int");

                    b.Property<int?>("MaxRooms")
                        .HasColumnType("int");

                    b.Property<double>("MonthlyCostGbp")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("YearlyCostGbp")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionTiers");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FuelCapacity")
                        .HasColumnType("int");

                    b.Property<int>("FuelTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("QuizId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FuelTypeId");

                    b.HasIndex("QuizId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.Question", b =>
                {
                    b.HasOne("RoadTrip.RoadTripDb.Database.Models.Quiz", null)
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.Room", b =>
                {
                    b.HasOne("RoadTrip.RoadTripDb.Database.Models.HostAppUser", null)
                        .WithMany("OpenRooms")
                        .HasForeignKey("HostAppUserRoadTripUserId");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.Vehicle", b =>
                {
                    b.HasOne("RoadTrip.RoadTripDb.Database.Models.FuelType", "FuelType")
                        .WithMany()
                        .HasForeignKey("FuelTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoadTrip.RoadTripDb.Database.Models.Quiz", null)
                        .WithMany("Vehicles")
                        .HasForeignKey("QuizId");

                    b.Navigation("FuelType");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.HostAppUser", b =>
                {
                    b.Navigation("OpenRooms");
                });

            modelBuilder.Entity("RoadTrip.RoadTripDb.Database.Models.Quiz", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}