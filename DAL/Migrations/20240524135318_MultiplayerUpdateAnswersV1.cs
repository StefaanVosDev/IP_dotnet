using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class MultiplayerUpdateAnswersV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnswerTextPlayer1",
                table: "Answers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswerTextPlayer2",
                table: "Answers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerTextPlayer1",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "AnswerTextPlayer2",
                table: "Answers");
        }
    }
}
