using Microsoft.EntityFrameworkCore.Migrations;

public partial class AddIsDarkModeColumn : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "IsDarkMode",
            table: "PrivacyConsents",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsDarkMode",
            table: "PrivacyConsents");
    }
} 