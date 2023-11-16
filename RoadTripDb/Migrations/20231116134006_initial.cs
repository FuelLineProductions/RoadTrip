using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadTripDb.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupSubscriptions",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HostsInSubscription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActiveSubscriptionTier = table.Column<int>(type: "int", nullable: false),
                    SubscriptionExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSubscriptions", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "GuestAppUsers",
                columns: table => new
                {
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActiveRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomInvites = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestAppUsers", x => x.GuestId);
                });

            migrationBuilder.CreateTable(
                name: "HostAppUsers",
                columns: table => new
                {
                    RoadTripUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostAppUsers", x => x.RoadTripUserId);
                });

            migrationBuilder.CreateTable(
                name: "IndividualSubscriptions",
                columns: table => new
                {
                    IndividualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Owner = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActiveSubscriptionTier = table.Column<int>(type: "int", nullable: false),
                    SubscriptionExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualSubscriptions", x => x.IndividualId);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionTier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearlyCostGbp = table.Column<double>(type: "float", nullable: false),
                    DiscountGbp = table.Column<double>(type: "float", nullable: false),
                    MonthlyCostGbp = table.Column<double>(type: "float", nullable: false),
                    MaxGuests = table.Column<int>(type: "int", nullable: true),
                    MaxRooms = table.Column<int>(type: "int", nullable: true),
                    MaxGameSaves = table.Column<int>(type: "int", nullable: true),
                    MaxHosts = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxUsers = table.Column<int>(type: "int", nullable: false),
                    CurrentUserCount = table.Column<int>(type: "int", nullable: false),
                    HostAppUserRoadTripUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_HostAppUsers_HostAppUserRoadTripUserId",
                        column: x => x.HostAppUserRoadTripUserId,
                        principalTable: "HostAppUsers",
                        principalColumn: "RoadTripUserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HostAppUserRoadTripUserId",
                table: "Rooms",
                column: "HostAppUserRoadTripUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupSubscriptions");

            migrationBuilder.DropTable(
                name: "GuestAppUsers");

            migrationBuilder.DropTable(
                name: "IndividualSubscriptions");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "SubscriptionTier");

            migrationBuilder.DropTable(
                name: "HostAppUsers");
        }
    }
}
