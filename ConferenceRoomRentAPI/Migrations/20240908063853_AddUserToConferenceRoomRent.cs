using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceRoomRentAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToConferenceRoomRent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ConferenceRoomRents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceRoomRents_UserId",
                table: "ConferenceRoomRents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConferenceRoomRents_AspNetUsers_UserId",
                table: "ConferenceRoomRents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceRoomRents_AspNetUsers_UserId",
                table: "ConferenceRoomRents");

            migrationBuilder.DropIndex(
                name: "IX_ConferenceRoomRents_UserId",
                table: "ConferenceRoomRents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ConferenceRoomRents");
        }
    }
}
