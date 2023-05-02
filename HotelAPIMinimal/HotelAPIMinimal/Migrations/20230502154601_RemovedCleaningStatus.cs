using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAPIMinimal.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCleaningStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CleaningStatus",
                table: "Room");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CleaningStatus",
                table: "Room",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
