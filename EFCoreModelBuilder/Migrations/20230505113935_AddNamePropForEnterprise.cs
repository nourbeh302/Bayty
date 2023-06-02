using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreModelBuilder.Migrations
{
    /// <inheritdoc />
    public partial class AddNamePropForEnterprise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Enterprises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Enterprises");
        }
    }
}
