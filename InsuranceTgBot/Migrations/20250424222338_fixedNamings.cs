using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceTgBot.Migrations
{
    /// <inheritdoc />
    public partial class fixedNamings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProvidedVehicleId",
                table: "UserProgresses",
                newName: "ProvidedVehicleIdentificationDocument");

            migrationBuilder.RenameColumn(
                name: "ProvidedID",
                table: "UserProgresses",
                newName: "ProvidedDriverLicense");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProvidedVehicleIdentificationDocument",
                table: "UserProgresses",
                newName: "ProvidedVehicleId");

            migrationBuilder.RenameColumn(
                name: "ProvidedDriverLicense",
                table: "UserProgresses",
                newName: "ProvidedID");
        }
    }
}
