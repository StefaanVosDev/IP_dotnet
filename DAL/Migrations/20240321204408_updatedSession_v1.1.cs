using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedSession_v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Sessions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Sessions",
                type: "text",
                nullable: true);
        }
    }
}
