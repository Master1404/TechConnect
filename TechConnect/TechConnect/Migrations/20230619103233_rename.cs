using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechConnect.Migrations
{
    /// <inheritdoc />
    public partial class rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecialVehicls",
                table: "SpecialVehicls");

            migrationBuilder.RenameTable(
                name: "SpecialVehicls",
                newName: "SpecialVehicles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecialVehicles",
                table: "SpecialVehicles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PhotoPath",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialVehicleModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoPath", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoPath_SpecialVehicles_SpecialVehicleModelId",
                        column: x => x.SpecialVehicleModelId,
                        principalTable: "SpecialVehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoPath_SpecialVehicleModelId",
                table: "PhotoPath",
                column: "SpecialVehicleModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoPath");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecialVehicles",
                table: "SpecialVehicles");

            migrationBuilder.RenameTable(
                name: "SpecialVehicles",
                newName: "SpecialVehicls");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecialVehicls",
                table: "SpecialVehicls",
                column: "Id");
        }
    }
}
