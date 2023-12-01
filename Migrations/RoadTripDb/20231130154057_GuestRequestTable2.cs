using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadTrip.Migrations.RoadTripDb
{
    /// <inheritdoc />
    public partial class GuestRequestTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestRequestJoinQuizzes_GuestAppUsers_GuestId",
                table: "GuestRequestJoinQuizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestRequestJoinQuizzes_Quizzes_QuizId",
                table: "GuestRequestJoinQuizzes");

            migrationBuilder.DropIndex(
                name: "IX_GuestRequestJoinQuizzes_GuestId",
                table: "GuestRequestJoinQuizzes");

            migrationBuilder.DropIndex(
                name: "IX_GuestRequestJoinQuizzes_QuizId",
                table: "GuestRequestJoinQuizzes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GuestRequestJoinQuizzes_GuestId",
                table: "GuestRequestJoinQuizzes",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestRequestJoinQuizzes_QuizId",
                table: "GuestRequestJoinQuizzes",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestRequestJoinQuizzes_GuestAppUsers_GuestId",
                table: "GuestRequestJoinQuizzes",
                column: "GuestId",
                principalTable: "GuestAppUsers",
                principalColumn: "GuestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuestRequestJoinQuizzes_Quizzes_QuizId",
                table: "GuestRequestJoinQuizzes",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
