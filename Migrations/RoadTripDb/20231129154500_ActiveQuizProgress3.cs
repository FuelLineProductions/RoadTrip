using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadTrip.Migrations.RoadTripDb
{
    /// <inheritdoc />
    public partial class ActiveQuizProgress3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.CreateTable(
                name: "ActiveQuizProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HostNameIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentQuestionId = table.Column<int>(type: "int", nullable: true),
                    CurrentAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentScore = table.Column<int>(type: "int", nullable: true),
                    CurrentFuel = table.Column<int>(type: "int", nullable: true),
                    StartedQuestion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FuelGuess = table.Column<int>(type: "int", nullable: true),
                    StartScore = table.Column<int>(type: "int", nullable: true),
                    CompletedScore = table.Column<int>(type: "int", nullable: true),
                    CompletedFuel = table.Column<int>(type: "int", nullable: true),
                    QuizStarted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QuizEnded = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveQuizProgresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreviousAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviousAnswers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveQuizProgresses");

            migrationBuilder.DropTable(
                name: "PreviousAnswers");

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuelChange = table.Column<int>(type: "int", nullable: false),
                    GivenAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    ScoreGained = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                });
        }
    }
}
