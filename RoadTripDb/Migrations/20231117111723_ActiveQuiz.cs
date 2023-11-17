using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadTripDb.Migrations
{
    /// <inheritdoc />
    public partial class ActiveQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Quizzes");
        }
    }
}
