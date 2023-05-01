using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAPIMinimal.Migrations
{
    /// <inheritdoc />
    public partial class RemovedHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Guest_GuestId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Hotel_HotelId",
                table: "Room");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_Room_HotelId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "GuestId",
                table: "Reservation",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_GuestId",
                table: "Reservation",
                newName: "IX_Reservation_UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Guest",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Guest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Guest_UserId",
                table: "Reservation",
                column: "UserId",
                principalTable: "Guest",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Guest_UserId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Guest");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reservation",
                newName: "GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation",
                newName: "IX_Reservation_GuestId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Guest",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Room_HotelId",
                table: "Room",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Guest_GuestId",
                table: "Reservation",
                column: "GuestId",
                principalTable: "Guest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Hotel_HotelId",
                table: "Room",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
