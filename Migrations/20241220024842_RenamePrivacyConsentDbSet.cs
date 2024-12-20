using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarMediaGroupTest.Migrations
{
    /// <inheritdoc />
    public partial class RenamePrivacyConsentDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_YourModels",
                table: "YourModels");

            migrationBuilder.RenameTable(
                name: "YourModels",
                newName: "PrivacyConsentModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivacyConsentModel",
                table: "PrivacyConsentModel",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivacyConsentModel",
                table: "PrivacyConsentModel");

            migrationBuilder.RenameTable(
                name: "PrivacyConsentModel",
                newName: "YourModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YourModels",
                table: "YourModels",
                column: "Id");
        }
    }
}
