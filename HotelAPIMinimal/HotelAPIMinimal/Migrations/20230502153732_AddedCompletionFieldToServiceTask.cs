using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAPIMinimal.Migrations
{
    /// <inheritdoc />
    public partial class AddedCompletionFieldToServiceTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "ServiceTask",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "ServiceTask");
        }
    }
}
