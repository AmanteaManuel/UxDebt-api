using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UxDebt.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    LabelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.LabelId);
                });

            migrationBuilder.CreateTable(
                name: "Repository",
                columns: table => new
                {
                    RepositoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GitId = table.Column<int>(type: "int", nullable: false),
                    HtmlUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repository", x => x.RepositoryId);
                });

            migrationBuilder.CreateTable(
                name: "Issue",
                columns: table => new
                {
                    IssueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GitId = table.Column<long>(type: "bigint", nullable: false),
                    HtmlUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discarded = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepositoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.IssueId);
                    table.ForeignKey(
                        name: "FK_Issue_Repository_RepositoryId",
                        column: x => x.RepositoryId,
                        principalTable: "Repository",
                        principalColumn: "RepositoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueLabel",
                columns: table => new
                {
                    LabelsIssueId = table.Column<int>(type: "int", nullable: false),
                    LabelsLabelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLabel", x => new { x.LabelsIssueId, x.LabelsLabelId });
                    table.ForeignKey(
                        name: "FK_IssueLabel_Issue_LabelsIssueId",
                        column: x => x.LabelsIssueId,
                        principalTable: "Issue",
                        principalColumn: "IssueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueLabel_Label_LabelsLabelId",
                        column: x => x.LabelsLabelId,
                        principalTable: "Label",
                        principalColumn: "LabelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issue_GitId",
                table: "Issue",
                column: "GitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issue_RepositoryId",
                table: "Issue",
                column: "RepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLabel_LabelsLabelId",
                table: "IssueLabel",
                column: "LabelsLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_Repository_GitId",
                table: "Repository",
                column: "GitId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueLabel");

            migrationBuilder.DropTable(
                name: "Issue");

            migrationBuilder.DropTable(
                name: "Label");

            migrationBuilder.DropTable(
                name: "Repository");
        }
    }
}
