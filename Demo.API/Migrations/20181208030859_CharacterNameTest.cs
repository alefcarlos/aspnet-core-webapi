using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.API.Migrations
{
    public partial class CharacterNameTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Character",
                type: "NVARCHAR(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Character",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(100)",
                oldNullable: true);
        }
    }
}
