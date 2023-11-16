using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadTripDb.Migrations
{
    /// <inheritdoc />
    public partial class Quizzes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionTier",
                table: "SubscriptionTier");

            migrationBuilder.RenameTable(
                name: "SubscriptionTier",
                newName: "SubscriptionTiers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionTiers",
                table: "SubscriptionTiers",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
