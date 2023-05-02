using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAPIMinimal.Migrations
{
    /// <inheritdoc />
    public partial class AddedNoteChangedToStatusService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "ServiceTask");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ServiceTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ServiceTask",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "ServiceTask");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ServiceTask");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "ServiceTask",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
