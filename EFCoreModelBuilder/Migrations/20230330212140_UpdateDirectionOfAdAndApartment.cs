using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreModelBuilder.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDirectionOfAdAndApartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Apartments_ApartmentId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_ApartmentId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Advertisements");

            migrationBuilder.AddColumn<int>(
                name: "AdvertisementId",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_AdvertisementId",
                table: "Apartments",
                column: "AdvertisementId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Advertisements_AdvertisementId",
                table: "Apartments",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Advertisements_AdvertisementId",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_AdvertisementId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "AdvertisementId",
                table: "Apartments");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "Advertisements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ApartmentId",
                table: "Advertisements",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Apartments_ApartmentId",
                table: "Advertisements",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
