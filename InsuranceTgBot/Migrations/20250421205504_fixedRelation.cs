using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceTgBot.Migrations
{
    /// <inheritdoc />
    public partial class fixedRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "HistoryRecords",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRecords_UserId1",
                table: "HistoryRecords",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRecords_Users_UserId1",
                table: "HistoryRecords",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRecords_Users_UserId1",
                table: "HistoryRecords");

            migrationBuilder.DropIndex(
                name: "IX_HistoryRecords_UserId1",
                table: "HistoryRecords");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "HistoryRecords");
        }
    }
}
