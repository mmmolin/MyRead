using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRead.Data.Migrations
{
    public partial class FontCoverFilePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverFilePath",
                table: "Book",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverFilePath",
                table: "Book");
        }
    }
}
