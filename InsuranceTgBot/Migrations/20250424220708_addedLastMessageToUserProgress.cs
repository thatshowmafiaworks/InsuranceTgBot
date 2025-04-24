using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceTgBot.Migrations
{
    /// <inheritdoc />
    public partial class addedLastMessageToUserProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastMessage",
                table: "UserProgresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMessage",
                table: "UserProgresses");
        }
    }
}
