using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreModelBuilder.Migrations
{
    /// <inheritdoc />
    public partial class AddApartmentForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Apartments_PropertyFeaturesId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Apartments_PropertyFeaturesId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Villas_Apartments_VillaFeaturesId",
                table: "Villas");

            migrationBuilder.RenameColumn(
                name: "VillaFeaturesId",
                table: "Villas",
                newName: "ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Villas_VillaFeaturesId",
                table: "Villas",
                newName: "IX_Villas_ApartmentId");

            migrationBuilder.RenameColumn(
                name: "PropertyFeaturesId",
                table: "Properties",
                newName: "ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_PropertyFeaturesId",
                table: "Properties",
                newName: "IX_Properties_ApartmentId");

            migrationBuilder.RenameColumn(
                name: "PropertyFeaturesId",
                table: "Buildings",
                newName: "ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_PropertyFeaturesId",
                table: "Buildings",
                newName: "IX_Buildings_ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Apartments_ApartmentId",
                table: "Buildings",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Apartments_ApartmentId",
                table: "Properties",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Villas_Apartments_ApartmentId",
                table: "Villas",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Apartments_ApartmentId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Apartments_ApartmentId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Villas_Apartments_ApartmentId",
                table: "Villas");

            migrationBuilder.RenameColumn(
                name: "ApartmentId",
                table: "Villas",
                newName: "VillaFeaturesId");

            migrationBuilder.RenameIndex(
                name: "IX_Villas_ApartmentId",
                table: "Villas",
                newName: "IX_Villas_VillaFeaturesId");

            migrationBuilder.RenameColumn(
                name: "ApartmentId",
                table: "Properties",
                newName: "PropertyFeaturesId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_ApartmentId",
                table: "Properties",
                newName: "IX_Properties_PropertyFeaturesId");

            migrationBuilder.RenameColumn(
                name: "ApartmentId",
                table: "Buildings",
                newName: "PropertyFeaturesId");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_ApartmentId",
                table: "Buildings",
                newName: "IX_Buildings_PropertyFeaturesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Apartments_PropertyFeaturesId",
                table: "Buildings",
                column: "PropertyFeaturesId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Apartments_PropertyFeaturesId",
                table: "Properties",
                column: "PropertyFeaturesId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Villas_Apartments_VillaFeaturesId",
                table: "Villas",
                column: "VillaFeaturesId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
