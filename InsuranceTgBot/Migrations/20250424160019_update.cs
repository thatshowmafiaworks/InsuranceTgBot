using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceTgBot.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDatas");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "VehicleDocuments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DriverLicenses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserProgresses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserTgId = table.Column<long>(type: "bigint", nullable: false),
                    AskedToProvideID = table.Column<bool>(type: "bit", nullable: false),
                    ProvidedID = table.Column<bool>(type: "bit", nullable: false),
                    AskedToProvideVehicleId = table.Column<bool>(type: "bit", nullable: false),
                    ProvidedVehicleId = table.Column<bool>(type: "bit", nullable: false),
                    AskedToPay = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    DriverLicenseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VehicleDocumentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProgresses_DriverLicenses_DriverLicenseId",
                        column: x => x.DriverLicenseId,
                        principalTable: "DriverLicenses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProgresses_VehicleDocuments_VehicleDocumentId",
                        column: x => x.VehicleDocumentId,
                        principalTable: "VehicleDocuments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDocuments_UserId",
                table: "VehicleDocuments",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverLicenses_UserId",
                table: "DriverLicenses",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProgresses_DriverLicenseId",
                table: "UserProgresses",
                column: "DriverLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgresses_UserId",
                table: "UserProgresses",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProgresses_VehicleDocumentId",
                table: "UserProgresses",
                column: "VehicleDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverLicenses_Users_UserId",
                table: "DriverLicenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleDocuments_Users_UserId",
                table: "VehicleDocuments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverLicenses_Users_UserId",
                table: "DriverLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleDocuments_Users_UserId",
                table: "VehicleDocuments");

            migrationBuilder.DropTable(
                name: "UserProgresses");

            migrationBuilder.DropIndex(
                name: "IX_VehicleDocuments_UserId",
                table: "VehicleDocuments");

            migrationBuilder.DropIndex(
                name: "IX_DriverLicenses_UserId",
                table: "DriverLicenses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VehicleDocuments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DriverLicenses");

            migrationBuilder.CreateTable(
                name: "UserDatas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DriverLicenseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehicleDocumentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDatas_DriverLicenses_DriverLicenseId",
                        column: x => x.DriverLicenseId,
                        principalTable: "DriverLicenses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserDatas_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDatas_VehicleDocuments_VehicleDocumentId",
                        column: x => x.VehicleDocumentId,
                        principalTable: "VehicleDocuments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDatas_DriverLicenseId",
                table: "UserDatas",
                column: "DriverLicenseId",
                unique: true,
                filter: "[DriverLicenseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserDatas_UserId",
                table: "UserDatas",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDatas_VehicleDocumentId",
                table: "UserDatas",
                column: "VehicleDocumentId",
                unique: true,
                filter: "[VehicleDocumentId] IS NOT NULL");
        }
    }
}
