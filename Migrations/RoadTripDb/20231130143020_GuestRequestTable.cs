using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadTrip.Migrations.RoadTripDb
{
    /// <inheritdoc />
    public partial class GuestRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuestRequestJoinQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestRequestJoinQuizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestRequestJoinQuizzes_GuestAppUsers_GuestId",
                        column: x => x.GuestId,
                        principalTable: "GuestAppUsers",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestRequestJoinQuizzes_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestRequestJoinQuizzes_GuestId",
                table: "GuestRequestJoinQuizzes",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestRequestJoinQuizzes_QuizId",
                table: "GuestRequestJoinQuizzes",
                column: "QuizId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestRequestJoinQuizzes");
        }
    }
}
