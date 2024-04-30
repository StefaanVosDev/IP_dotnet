using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlowId",
                table: "Answers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_FlowId",
                table: "Answers",
                column: "FlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Flows_FlowId",
                table: "Answers",
                column: "FlowId",
                principalTable: "Flows",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Flows_FlowId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_FlowId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "FlowId",
                table: "Answers");
        }
    }
}
