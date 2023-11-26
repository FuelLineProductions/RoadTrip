using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadTrip.Migrations.RoadTripDb
{
    /// <inheritdoc />
    public partial class Answer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    GivenAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScoreGained = table.Column<int>(type: "int", nullable: false),
                    FuelChange = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    FuelUsed = table.Column<int>(type: "int", nullable: false),
                    FuelGuess = table.Column<int>(type: "int", nullable: false),
                    QuestionsAnswered = table.Column<int>(type: "int", nullable: false),
                    QuestionsCorrect = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "QuizResults");
        }
    }
}