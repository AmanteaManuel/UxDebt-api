using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UxDebt.Migrations
{
    public partial class TagAndIssueTagCorrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTag_Issue_IssueId",
                table: "IssueTag");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueTag_Tags_TagId",
                table: "IssueTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueTag",
                table: "IssueTag");

            migrationBuilder.RenameTable(
                name: "IssueTag",
                newName: "IssueTags");

            migrationBuilder.RenameIndex(
                name: "IX_IssueTag_TagId",
                table: "IssueTags",
                newName: "IX_IssueTags_TagId");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IssueTagId",
                table: "IssueTags",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueTags",
                table: "IssueTags",
                columns: new[] { "IssueId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTags_Issue_IssueId",
                table: "IssueTags",
                column: "IssueId",
                principalTable: "Issue",
                principalColumn: "IssueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTags_Tags_TagId",
                table: "IssueTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTags_Issue_IssueId",
                table: "IssueTags");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueTags_Tags_TagId",
                table: "IssueTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueTags",
                table: "IssueTags");

            migrationBuilder.DropColumn(
                name: "IssueTagId",
                table: "IssueTags");

            migrationBuilder.RenameTable(
                name: "IssueTags",
                newName: "IssueTag");

            migrationBuilder.RenameIndex(
                name: "IX_IssueTags_TagId",
                table: "IssueTag",
                newName: "IX_IssueTag_TagId");

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "Tags",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueTag",
                table: "IssueTag",
                columns: new[] { "IssueId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTag_Issue_IssueId",
                table: "IssueTag",
                column: "IssueId",
                principalTable: "Issue",
                principalColumn: "IssueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTag_Tags_TagId",
                table: "IssueTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
