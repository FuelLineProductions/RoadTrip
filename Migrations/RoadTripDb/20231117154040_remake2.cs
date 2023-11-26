using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadTrip.Migrations.RoadTripDb
{
    /// <inheritdoc />
    public partial class remake2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_QuizId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_HostAppUsers_HostAppUserRoadTripUserId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_FuelTypes_FuelTypeId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Quizzes_QuizId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_FuelTypeId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_QuizId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HostAppUserRoadTripUserId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Questions_QuizId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HostAppUserRoadTripUserId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomInvites",
                table: "GuestAppUsers");

            migrationBuilder.DropColumn(
                name: "HostsInSubscription",
                table: "GroupSubscriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuizId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HostAppUserRoadTripUserId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomInvites",
                table: "GuestAppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HostsInSubscription",
                table: "GroupSubscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_FuelTypeId",
                table: "Vehicles",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_QuizId",
                table: "Vehicles",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HostAppUserRoadTripUserId",
                table: "Rooms",
                column: "HostAppUserRoadTripUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_QuizId",
                table: "Questions",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_HostAppUsers_HostAppUserRoadTripUserId",
                table: "Rooms",
                column: "HostAppUserRoadTripUserId",
                principalTable: "HostAppUsers",
                principalColumn: "RoadTripUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_FuelTypes_FuelTypeId",
                table: "Vehicles",
                column: "FuelTypeId",
                principalTable: "FuelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Quizzes_QuizId",
                table: "Vehicles",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }
    }
}