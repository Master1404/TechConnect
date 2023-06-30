using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechConnect.Migrations
{
    /// <inheritdoc />
    public partial class NewDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoPath_SpecialVehicles_SpecialVehicleId",
                table: "PhotoPath");

            migrationBuilder.DropIndex(
                name: "IX_PhotoPath_SpecialVehicleId",
                table: "PhotoPath");

            migrationBuilder.DropColumn(
                name: "SpecialVehicleId",
                table: "PhotoPath");

            migrationBuilder.AddColumn<int>(
                name: "SpecialVehicleModelId",
                table: "PhotoPath",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoPath_SpecialVehicleModelId",
                table: "PhotoPath",
                column: "SpecialVehicleModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoPath_SpecialVehicles_SpecialVehicleModelId",
                table: "PhotoPath",
                column: "SpecialVehicleModelId",
                principalTable: "SpecialVehicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoPath_SpecialVehicles_SpecialVehicleModelId",
                table: "PhotoPath");

            migrationBuilder.DropIndex(
                name: "IX_PhotoPath_SpecialVehicleModelId",
                table: "PhotoPath");

            migrationBuilder.DropColumn(
                name: "SpecialVehicleModelId",
                table: "PhotoPath");

            migrationBuilder.AddColumn<int>(
                name: "SpecialVehicleId",
                table: "PhotoPath",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoPath_SpecialVehicleId",
                table: "PhotoPath",
                column: "SpecialVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoPath_SpecialVehicles_SpecialVehicleId",
                table: "PhotoPath",
                column: "SpecialVehicleId",
                principalTable: "SpecialVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
