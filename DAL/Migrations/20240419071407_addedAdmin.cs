using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Mediaid",
                table: "Question",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mediaid",
                table: "Flows",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    url = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_Mediaid",
                table: "Question",
                column: "Mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_Flows_Mediaid",
                table: "Flows",
                column: "Mediaid");

            migrationBuilder.AddForeignKey(
                name: "FK_Flows_Media_Mediaid",
                table: "Flows",
                column: "Mediaid",
                principalTable: "Media",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Media_Mediaid",
                table: "Question",
                column: "Mediaid",
                principalTable: "Media",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flows_Media_Mediaid",
                table: "Flows");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Media_Mediaid",
                table: "Question");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Question_Mediaid",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Flows_Mediaid",
                table: "Flows");

            migrationBuilder.DropColumn(
                name: "Mediaid",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Mediaid",
                table: "Flows");
        }
    }
}
