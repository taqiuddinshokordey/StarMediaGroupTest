using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarMediaGroupTest.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDarkModeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDarkMode",
                table: "PrivacyConsentModel",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDarkMode",
                table: "PrivacyConsentModel");
        }
    }
}
