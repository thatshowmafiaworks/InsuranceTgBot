using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceTgBot.Migrations
{
    /// <inheritdoc />
    public partial class fixedTablesContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProgresses_DriverLicenses_DriverLicenseId",
                table: "UserProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProgresses_VehicleDocuments_VehicleDocumentId",
                table: "UserProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserProgresses_DriverLicenseId",
                table: "UserProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserProgresses_VehicleDocumentId",
                table: "UserProgresses");

            migrationBuilder.DropColumn(
                name: "AskedToPay",
                table: "UserProgresses");

            migrationBuilder.DropColumn(
                name: "AskedToProvideID",
                table: "UserProgresses");

            migrationBuilder.DropColumn(
                name: "AskedToProvideVehicleId",
                table: "UserProgresses");

            migrationBuilder.DropColumn(
                name: "DriverLicenseId",
                table: "UserProgresses");

            migrationBuilder.DropColumn(
                name: "VehicleDocumentId",
                table: "UserProgresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AskedToPay",
                table: "UserProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AskedToProvideID",
                table: "UserProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AskedToProvideVehicleId",
                table: "UserProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DriverLicenseId",
                table: "UserProgresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleDocumentId",
                table: "UserProgresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProgresses_DriverLicenseId",
                table: "UserProgresses",
                column: "DriverLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgresses_VehicleDocumentId",
                table: "UserProgresses",
                column: "VehicleDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgresses_DriverLicenses_DriverLicenseId",
                table: "UserProgresses",
                column: "DriverLicenseId",
                principalTable: "DriverLicenses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgresses_VehicleDocuments_VehicleDocumentId",
                table: "UserProgresses",
                column: "VehicleDocumentId",
                principalTable: "VehicleDocuments",
                principalColumn: "Id");
        }
    }
}
