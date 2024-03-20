using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSubclasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flow_Flow_ParentFlowId",
                table: "Flow");

            migrationBuilder.DropForeignKey(
                name: "FK_Flow_Project_ProjectId",
                table: "Flow");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Flow_FlowId",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flow",
                table: "Flow");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameTable(
                name: "Flow",
                newName: "Flows");

            migrationBuilder.RenameIndex(
                name: "IX_Flow_ProjectId",
                table: "Flows",
                newName: "IX_Flows_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Flow_ParentFlowId",
                table: "Flows",
                newName: "IX_Flows_ParentFlowId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Question",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Max",
                table: "Question",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Min",
                table: "Question",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Options",
                table: "Question",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "SingleChoiceQuestion_Options",
                table: "Question",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flows",
                table: "Flows",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MultipleChoiceAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SelectedOptions = table.Column<List<string>>(type: "text[]", nullable: true),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
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
                    Text = table.Column<string>(type: "text", nullable: true),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
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
                    SelectedValue = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
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
                    SelectedOption = table.Column<string>(type: "text", nullable: true),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Flows_Flows_ParentFlowId",
                table: "Flows",
                column: "ParentFlowId",
                principalTable: "Flows",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flows_Projects_ProjectId",
                table: "Flows",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Flows_FlowId",
                table: "Question",
                column: "FlowId",
                principalTable: "Flows",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flows_Flows_ParentFlowId",
                table: "Flows");

            migrationBuilder.DropForeignKey(
                name: "FK_Flows_Projects_ProjectId",
                table: "Flows");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Flows_FlowId",
                table: "Question");

            migrationBuilder.DropTable(
                name: "MultipleChoiceAnswers");

            migrationBuilder.DropTable(
                name: "OpenAnswers");

            migrationBuilder.DropTable(
                name: "RangeAnswers");

            migrationBuilder.DropTable(
                name: "SingleChoiceAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flows",
                table: "Flows");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Max",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Min",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Options",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "SingleChoiceQuestion_Options",
                table: "Question");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameTable(
                name: "Flows",
                newName: "Flow");

            migrationBuilder.RenameIndex(
                name: "IX_Flows_ProjectId",
                table: "Flow",
                newName: "IX_Flow_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Flows_ParentFlowId",
                table: "Flow",
                newName: "IX_Flow_ParentFlowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flow",
                table: "Flow",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flow_Flow_ParentFlowId",
                table: "Flow",
                column: "ParentFlowId",
                principalTable: "Flow",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flow_Project_ProjectId",
                table: "Flow",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Flow_FlowId",
                table: "Question",
                column: "FlowId",
                principalTable: "Flow",
                principalColumn: "Id");
        }
    }
}
