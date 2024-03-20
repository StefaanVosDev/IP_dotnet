using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Migration12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flows_Flows_FlowId1",
                table: "Flows");

            migrationBuilder.DropForeignKey(
                name: "FK_Flows_Installations_InstallationId",
                table: "Flows");

            migrationBuilder.DropForeignKey(
                name: "FK_Flows_Projects_ProjectId",
                table: "Flows");

            migrationBuilder.DropForeignKey(
                name: "FK_Flows_Sessions_SessionId",
                table: "Flows");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectAdministrator_ProjectAdministratorId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "DataAnalyses");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Installations");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "PlatformProject");

            migrationBuilder.DropTable(
                name: "ProjectAdministrator");

            migrationBuilder.DropTable(
                name: "Respondents");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "PlatformAdministrator");

            migrationBuilder.DropTable(
                name: "Facilitators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectAdministratorId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flows",
                table: "Flows");

            migrationBuilder.DropIndex(
                name: "IX_Flows_InstallationId",
                table: "Flows");

            migrationBuilder.DropIndex(
                name: "IX_Flows_SessionId",
                table: "Flows");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FlowId",
                table: "Flows");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Flows");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "Flows");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Flows");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameTable(
                name: "Flows",
                newName: "Flow");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Project",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProjectAdministratorId",
                table: "Project",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Flow",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FlowId1",
                table: "Flow",
                newName: "ParentFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_Flows_ProjectId",
                table: "Flow",
                newName: "IX_Flow_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Flows_FlowId1",
                table: "Flow",
                newName: "IX_Flow_ParentFlowId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Project",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Flow",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flow",
                table: "Flow",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: true),
                    FlowId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Flow_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flow",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_FlowId",
                table: "Question",
                column: "FlowId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flow_Flow_ParentFlowId",
                table: "Flow");

            migrationBuilder.DropForeignKey(
                name: "FK_Flow_Project_ProjectId",
                table: "Flow");

            migrationBuilder.DropTable(
                name: "Question");

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

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Projects",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Projects",
                newName: "ProjectAdministratorId");

            migrationBuilder.RenameColumn(
                name: "ParentFlowId",
                table: "Flows",
                newName: "FlowId1");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Flows",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Flow_ProjectId",
                table: "Flows",
                newName: "IX_Flows_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Flow_ParentFlowId",
                table: "Flows",
                newName: "IX_Flows_FlowId1");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectAdministratorId",
                table: "Projects",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "Flows",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "FlowId",
                table: "Flows",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Flows",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstallationId",
                table: "Flows",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Flows",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flows",
                table: "Flows",
                column: "FlowId");

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlowId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_Contents_Flows_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flows",
                        principalColumn: "FlowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facilitators",
                columns: table => new
                {
                    FacilitatorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilitators", x => x.FacilitatorId);
                });

            migrationBuilder.CreateTable(
                name: "PlatformAdministrator",
                columns: table => new
                {
                    PlatformAdministratorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformAdministrator", x => x.PlatformAdministratorId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlowId = table.Column<int>(type: "integer", nullable: false),
                    AnswerId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId1 = table.Column<int>(type: "integer", nullable: true),
                    QuestionText = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_Flows_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flows",
                        principalColumn: "FlowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Questions_QuestionId1",
                        column: x => x.QuestionId1,
                        principalTable: "Questions",
                        principalColumn: "QuestionId");
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FacilitatorId = table.Column<int>(type: "integer", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Sessions_Facilitators_FacilitatorId",
                        column: x => x.FacilitatorId,
                        principalTable: "Facilitators",
                        principalColumn: "FacilitatorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    PlatformId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlatformAdministratorId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.PlatformId);
                    table.ForeignKey(
                        name: "FK_Platforms_PlatformAdministrator_PlatformAdministratorId",
                        column: x => x.PlatformAdministratorId,
                        principalTable: "PlatformAdministrator",
                        principalColumn: "PlatformAdministratorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectAdministrator",
                columns: table => new
                {
                    ProjectAdministratorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlatformAdministratorId = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Organisation = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAdministrator", x => x.ProjectAdministratorId);
                    table.ForeignKey(
                        name: "FK_ProjectAdministrator_PlatformAdministrator_PlatformAdminist~",
                        column: x => x.PlatformAdministratorId,
                        principalTable: "PlatformAdministrator",
                        principalColumn: "PlatformAdministratorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "integer", nullable: false),
                    FacilitatorId = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_Facilitators_FacilitatorId",
                        column: x => x.FacilitatorId,
                        principalTable: "Facilitators",
                        principalColumn: "FacilitatorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Questions_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Respondents",
                columns: table => new
                {
                    RespondentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FacilitatorId = table.Column<int>(type: "integer", nullable: false),
                    SessionId = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respondents", x => x.RespondentId);
                    table.ForeignKey(
                        name: "FK_Respondents_Facilitators_FacilitatorId",
                        column: x => x.FacilitatorId,
                        principalTable: "Facilitators",
                        principalColumn: "FacilitatorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Respondents_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatformProject",
                columns: table => new
                {
                    PlatformsPlatformId = table.Column<int>(type: "integer", nullable: false),
                    ProjectsProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformProject", x => new { x.PlatformsPlatformId, x.ProjectsProjectId });
                    table.ForeignKey(
                        name: "FK_PlatformProject_Platforms_PlatformsPlatformId",
                        column: x => x.PlatformsPlatformId,
                        principalTable: "Platforms",
                        principalColumn: "PlatformId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatformProject_Projects_ProjectsProjectId",
                        column: x => x.ProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataAnalyses",
                columns: table => new
                {
                    AnalysisId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectAdministratorId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataAnalyses", x => x.AnalysisId);
                    table.ForeignKey(
                        name: "FK_DataAnalyses_ProjectAdministrator_ProjectAdministratorId",
                        column: x => x.ProjectAdministratorId,
                        principalTable: "ProjectAdministrator",
                        principalColumn: "ProjectAdministratorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataAnalyses_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataAnalyses_Projects_ProjectId1",
                        column: x => x.ProjectId1,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    RespondentId = table.Column<int>(type: "integer", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "RespondentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RespondentId = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "RespondentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Installations",
                columns: table => new
                {
                    InstallationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FacilitatorId = table.Column<int>(type: "integer", nullable: false),
                    RespondentId = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installations", x => x.InstallationId);
                    table.ForeignKey(
                        name: "FK_Installations_Facilitators_FacilitatorId",
                        column: x => x.FacilitatorId,
                        principalTable: "Facilitators",
                        principalColumn: "FacilitatorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Installations_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "RespondentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectAdministratorId",
                table: "Projects",
                column: "ProjectAdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Flows_InstallationId",
                table: "Flows",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_Flows_SessionId",
                table: "Flows",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_RespondentId",
                table: "Answers",
                column: "RespondentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_FlowId",
                table: "Contents",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_DataAnalyses_ProjectAdministratorId",
                table: "DataAnalyses",
                column: "ProjectAdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_DataAnalyses_ProjectId",
                table: "DataAnalyses",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DataAnalyses_ProjectId1",
                table: "DataAnalyses",
                column: "ProjectId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_RespondentId",
                table: "Feedbacks",
                column: "RespondentId");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_FacilitatorId",
                table: "Installations",
                column: "FacilitatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_RespondentId",
                table: "Installations",
                column: "RespondentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_FacilitatorId",
                table: "Notes",
                column: "FacilitatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformProject_ProjectsProjectId",
                table: "PlatformProject",
                column: "ProjectsProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_PlatformAdministratorId",
                table: "Platforms",
                column: "PlatformAdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAdministrator_PlatformAdministratorId",
                table: "ProjectAdministrator",
                column: "PlatformAdministratorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_FlowId",
                table: "Questions",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionId1",
                table: "Questions",
                column: "QuestionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Respondents_FacilitatorId",
                table: "Respondents",
                column: "FacilitatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Respondents_SessionId",
                table: "Respondents",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_FacilitatorId",
                table: "Sessions",
                column: "FacilitatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flows_Flows_FlowId1",
                table: "Flows",
                column: "FlowId1",
                principalTable: "Flows",
                principalColumn: "FlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flows_Installations_InstallationId",
                table: "Flows",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "InstallationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flows_Projects_ProjectId",
                table: "Flows",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flows_Sessions_SessionId",
                table: "Flows",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectAdministrator_ProjectAdministratorId",
                table: "Projects",
                column: "ProjectAdministratorId",
                principalTable: "ProjectAdministrator",
                principalColumn: "ProjectAdministratorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
