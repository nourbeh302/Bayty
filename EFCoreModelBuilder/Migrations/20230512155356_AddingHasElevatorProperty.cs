using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreModelBuilder.Migrations
{
    /// <inheritdoc />
    public partial class AddingHasElevatorProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasElevator",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasElevator",
                table: "Apartments");
        }
    }
}
