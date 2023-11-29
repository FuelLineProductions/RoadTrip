using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadTrip.Migrations.RoadTripDb
{
    /// <inheritdoc />
    public partial class ChangeGuest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveRoomId",
                table: "GuestAppUsers");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "GuestAppUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "GuestId",
                table: "ActiveQuizProgresses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActiveRoomId",
                table: "GuestAppUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HostId",
                table: "GuestAppUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "GuestId",
                table: "ActiveQuizProgresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
