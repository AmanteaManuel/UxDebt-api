using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UxDebt.Migrations
{
    public partial class ownerTorepo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Repository",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Repository");
        }
    }
}
