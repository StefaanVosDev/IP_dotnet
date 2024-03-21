using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultipleChoiceAnswers");

            migrationBuilder.DropTable(
                name: "OpenAnswers");

            migrationBuilder.DropTable(
                name: "RangeAnswers");

            migrationBuilder.DropTable(
                name: "SingleChoiceAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.AlterColumn<int>(
                name: "FlowId",
                table: "Sessions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: true),
                    SessionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SessionId",
                table: "Answers",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Sessions");

            migrationBuilder.AlterColumn<int>(
                name: "FlowId",
                table: "Sessions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "FlowId");

            migrationBuilder.CreateTable(
                name: "MultipleChoiceAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    SelectedOptions = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceAnswers_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenAnswers_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RangeAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    SelectedValue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangeAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RangeAnswers_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingleChoiceAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    SelectedOption = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChoiceAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleChoiceAnswers_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceAnswers_QuestionId",
                table: "MultipleChoiceAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenAnswers_QuestionId",
                table: "OpenAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_RangeAnswers_QuestionId",
                table: "RangeAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChoiceAnswers_QuestionId",
                table: "SingleChoiceAnswers",
                column: "QuestionId");
        }
    }
}
