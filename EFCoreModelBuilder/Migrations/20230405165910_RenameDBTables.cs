using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreModelBuilder.Migrations
{
    /// <inheritdoc />
    public partial class RenameDBTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Advertisements_AdvertisementId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Apartments_ApartmentId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Villas_Apartments_ApartmentId",
                table: "Villas");

            migrationBuilder.DropTable(
                name: "ApartmentImagePath");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_AdvertisementId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "AdvertisementId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Bathrooms Count",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "ApartmentId",
                table: "Villas",
                newName: "HouseBaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Villas_ApartmentId",
                table: "Villas",
                newName: "IX_Villas_HouseBaseId");

            migrationBuilder.RenameColumn(
                name: "ApartmentId",
                table: "Buildings",
                newName: "HouseBaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_ApartmentId",
                table: "Buildings",
                newName: "IX_Buildings_HouseBaseId");

            migrationBuilder.RenameColumn(
                name: "Rooms Count",
                table: "Apartments",
                newName: "HouseBaseId");

            migrationBuilder.RenameColumn(
                name: "Kitchens Count",
                table: "Apartments",
                newName: "FloorNumber");

            migrationBuilder.AddColumn<bool>(
                name: "IsFurnished",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVitalSite",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "HouseBases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    RoomsCount = table.Column<int>(name: "Rooms Count", type: "int", nullable: false),
                    KitchensCount = table.Column<int>(name: "Kitchens Count", type: "int", nullable: false),
                    BathroomsCount = table.Column<int>(name: "Bathrooms Count", type: "int", nullable: false),
                    AdvertisementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseBases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseBases_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseBaseImagePath",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(name: "Image Path", type: "nvarchar(450)", nullable: false),
                    HouseBaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseBaseImagePath", x => new { x.Id, x.ImagePath });
                    table.ForeignKey(
                        name: "FK_HouseBaseImagePath_HouseBases_HouseBaseId",
                        column: x => x.HouseBaseId,
                        principalTable: "HouseBases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_HouseBaseId",
                table: "Apartments",
                column: "HouseBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseBaseImagePath_HouseBaseId",
                table: "HouseBaseImagePath",
                column: "HouseBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseBases_AdvertisementId",
                table: "HouseBases",
                column: "AdvertisementId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_HouseBases_HouseBaseId",
                table: "Apartments",
                column: "HouseBaseId",
                principalTable: "HouseBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_HouseBases_HouseBaseId",
                table: "Buildings",
                column: "HouseBaseId",
                principalTable: "HouseBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Villas_HouseBases_HouseBaseId",
                table: "Villas",
                column: "HouseBaseId",
                principalTable: "HouseBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_HouseBases_HouseBaseId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_HouseBases_HouseBaseId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Villas_HouseBases_HouseBaseId",
                table: "Villas");

            migrationBuilder.DropTable(
                name: "HouseBaseImagePath");

            migrationBuilder.DropTable(
                name: "HouseBases");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_HouseBaseId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "IsFurnished",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "IsVitalSite",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "HouseBaseId",
                table: "Villas",
                newName: "ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Villas_HouseBaseId",
                table: "Villas",
                newName: "IX_Villas_ApartmentId");

            migrationBuilder.RenameColumn(
                name: "HouseBaseId",
                table: "Buildings",
                newName: "ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_HouseBaseId",
                table: "Buildings",
                newName: "IX_Buildings_ApartmentId");

            migrationBuilder.RenameColumn(
                name: "HouseBaseId",
                table: "Apartments",
                newName: "Rooms Count");

            migrationBuilder.RenameColumn(
                name: "FloorNumber",
                table: "Apartments",
                newName: "Kitchens Count");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AdvertisementId",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Bathrooms Count",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Apartments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "ApartmentImagePath",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(name: "Image Path", type: "nvarchar(450)", nullable: false),
                    ApartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentImagePath", x => new { x.Id, x.ImagePath });
                    table.ForeignKey(
                        name: "FK_ApartmentImagePath_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    IsFurnished = table.Column<bool>(type: "bit", nullable: false),
                    IsVitalSite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_AdvertisementId",
                table: "Apartments",
                column: "AdvertisementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentImagePath_ApartmentId",
                table: "ApartmentImagePath",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ApartmentId",
                table: "Properties",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Advertisements_AdvertisementId",
                table: "Apartments",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Apartments_ApartmentId",
                table: "Buildings",
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
    }
}
