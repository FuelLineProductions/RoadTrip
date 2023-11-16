using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadTripDb.Migrations
{
    /// <inheritdoc />
    public partial class Quizzes3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizVechicles",
                table: "QuizVehicles");

            migrationBuilder.RenameTable(
                name: "QuizVehicles",
                newName: "QuizVehicles");

            migrationBuilder.RenameColumn(
                name: "VechicleId",
                table: "QuizVehicles",
                newName: "VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizVehicles",
                table: "QuizVehicles",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizVechicles",
                table: "QuizVehicles");

            migrationBuilder.RenameTable(
                name: "QuizVehicles",
                newName: "QuizVehicles");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "QuizVehicles",
                newName: "VechicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizVehicles",
                table: "QuizVehicles",
                column: "Id");
        }
    }
}
