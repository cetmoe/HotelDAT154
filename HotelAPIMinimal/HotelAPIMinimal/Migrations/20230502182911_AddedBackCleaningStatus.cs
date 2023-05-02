using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAPIMinimal.Migrations
{
    /// <inheritdoc />
    public partial class AddedBackCleaningStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CleaningStatus",
                table: "Room",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CleaningStatus",
                table: "Room");
        }
    }
}
