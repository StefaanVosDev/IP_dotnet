using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Flows_FlowId",
                table: "Question");

            migrationBuilder.AlterColumn<int>(
                name: "FlowId",
                table: "Question",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Question",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Flows_FlowId",
                table: "Question",
                column: "FlowId",
                principalTable: "Flows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Flows_FlowId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Question");

            migrationBuilder.AlterColumn<int>(
                name: "FlowId",
                table: "Question",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Flows_FlowId",
                table: "Question",
                column: "FlowId",
                principalTable: "Flows",
                principalColumn: "Id");
        }
    }
}
