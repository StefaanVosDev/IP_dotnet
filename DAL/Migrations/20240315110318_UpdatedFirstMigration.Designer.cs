﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(PhygitalDbContext))]
    [Migration("20240315110318_UpdatedFirstMigration")]
    partial class UpdatedFirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BL.Domain.Content", b =>
                {
                    b.Property<int>("ContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ContentId"));

                    b.Property<int>("FlowId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("ContentId");

                    b.HasIndex("FlowId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("BL.Domain.DataAnalysis", b =>
                {
                    b.Property<int>("AnalysisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AnalysisId"));

                    b.Property<int>("ProjectAdministratorId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int?>("ProjectId1")
                        .HasColumnType("integer");

                    b.HasKey("AnalysisId");

                    b.HasIndex("ProjectAdministratorId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectId1")
                        .IsUnique();

                    b.ToTable("DataAnalyses");
                });

            modelBuilder.Entity("BL.Domain.Facilitator", b =>
                {
                    b.Property<int>("FacilitatorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FacilitatorId"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("FacilitatorId");

                    b.ToTable("Facilitators");
                });

            modelBuilder.Entity("BL.Domain.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FeedbackId"));

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<int>("RespondentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("FeedbackId");

                    b.HasIndex("RespondentId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("BL.Domain.Flow", b =>
                {
                    b.Property<int>("FlowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FlowId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("FlowId1")
                        .HasColumnType("integer");

                    b.Property<int>("InstallationId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("SessionId")
                        .HasColumnType("integer");

                    b.Property<string>("Theme")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("FlowId");

                    b.HasIndex("FlowId1");

                    b.HasIndex("InstallationId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SessionId");

                    b.ToTable("Flows");
                });

            modelBuilder.Entity("BL.Domain.Installation", b =>
                {
                    b.Property<int>("InstallationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("InstallationId"));

                    b.Property<int>("FacilitatorId")
                        .HasColumnType("integer");

                    b.Property<int>("RespondentId")
                        .HasColumnType("integer");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("InstallationId");

                    b.HasIndex("FacilitatorId");

                    b.HasIndex("RespondentId");

                    b.ToTable("Installations");
                });

            modelBuilder.Entity("BL.Domain.Notes", b =>
                {
                    b.Property<int>("NoteId")
                        .HasColumnType("integer");

                    b.Property<int>("FacilitatorId")
                        .HasColumnType("integer");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.HasKey("NoteId");

                    b.HasIndex("FacilitatorId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("BL.Domain.Platform", b =>
                {
                    b.Property<int>("PlatformId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PlatformId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("PlatformAdministratorId")
                        .HasColumnType("integer");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("PlatformId");

                    b.HasIndex("PlatformAdministratorId");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("BL.Domain.PlatformAdministrator", b =>
                {
                    b.Property<int>("PlatformAdministratorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PlatformAdministratorId"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("PlatformAdministratorId");

                    b.ToTable("PlatformAdministrator");
                });

            modelBuilder.Entity("BL.Domain.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProjectId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ProjectAdministratorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("ProjectId");

                    b.HasIndex("ProjectAdministratorId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("BL.Domain.ProjectAdministrator", b =>
                {
                    b.Property<int>("ProjectAdministratorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProjectAdministratorId"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Organisation")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("PlatformAdministratorId")
                        .HasColumnType("integer");

                    b.HasKey("ProjectAdministratorId");

                    b.HasIndex("PlatformAdministratorId")
                        .IsUnique();

                    b.ToTable("ProjectAdministrator");
                });

            modelBuilder.Entity("BL.Domain.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("QuestionId"));

                    b.Property<int>("FlowId")
                        .HasColumnType("integer");

                    b.Property<int?>("QuestionId1")
                        .HasColumnType("integer");

                    b.Property<string>("QuestionText")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("QuestionId");

                    b.HasIndex("FlowId");

                    b.HasIndex("QuestionId1");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("BL.Domain.Respondent", b =>
                {
                    b.Property<int>("RespondentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RespondentId"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("FacilitatorId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("SessionId")
                        .HasColumnType("integer");

                    b.HasKey("RespondentId");

                    b.HasIndex("FacilitatorId");

                    b.HasIndex("SessionId");

                    b.ToTable("Respondents");
                });

            modelBuilder.Entity("BL.Domain.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SessionId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FacilitatorId")
                        .HasColumnType("integer");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("SessionId");

                    b.HasIndex("FacilitatorId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("PlatformProject", b =>
                {
                    b.Property<int>("PlatformsPlatformId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectsProjectId")
                        .HasColumnType("integer");

                    b.HasKey("PlatformsPlatformId", "ProjectsProjectId");

                    b.HasIndex("ProjectsProjectId");

                    b.ToTable("PlatformProject");
                });

            modelBuilder.Entity("BL.Domain.Content", b =>
                {
                    b.HasOne("BL.Domain.Flow", "Flow")
                        .WithMany("Contents")
                        .HasForeignKey("FlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flow");
                });

            modelBuilder.Entity("BL.Domain.DataAnalysis", b =>
                {
                    b.HasOne("BL.Domain.ProjectAdministrator", "ProjectAdministrator")
                        .WithMany("DataAnalyses")
                        .HasForeignKey("ProjectAdministratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BL.Domain.Project", "Project")
                        .WithMany("DataAnalyses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BL.Domain.Project", null)
                        .WithOne("DataAnalysis")
                        .HasForeignKey("BL.Domain.DataAnalysis", "ProjectId1");

                    b.Navigation("Project");

                    b.Navigation("ProjectAdministrator");
                });

            modelBuilder.Entity("BL.Domain.Feedback", b =>
                {
                    b.HasOne("BL.Domain.Respondent", "Respondent")
                        .WithMany("Feedback")
                        .HasForeignKey("RespondentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Respondent");
                });

            modelBuilder.Entity("BL.Domain.Flow", b =>
                {
                    b.HasOne("BL.Domain.Flow", null)
                        .WithMany("SubFlows")
                        .HasForeignKey("FlowId1");

                    b.HasOne("BL.Domain.Installation", "Installation")
                        .WithMany("Flows")
                        .HasForeignKey("InstallationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BL.Domain.Project", "Project")
                        .WithMany("Flows")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BL.Domain.Session", "Session")
                        .WithMany("Flows")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Installation");

                    b.Navigation("Project");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("BL.Domain.Installation", b =>
                {
                    b.HasOne("BL.Domain.Facilitator", "Facilitator")
                        .WithMany("Installations")
                        .HasForeignKey("FacilitatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BL.Domain.Respondent", "Respondent")
                        .WithMany("Installations")
                        .HasForeignKey("RespondentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facilitator");

                    b.Navigation("Respondent");
                });

            modelBuilder.Entity("BL.Domain.Notes", b =>
                {
                    b.HasOne("BL.Domain.Facilitator", "Facilitator")
                        .WithMany("Notes")
                        .HasForeignKey("FacilitatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BL.Domain.Question", "Question")
                        .WithMany("Notes")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facilitator");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("BL.Domain.Platform", b =>
                {
                    b.HasOne("BL.Domain.PlatformAdministrator", "PlatformAdministrator")
                        .WithMany("Platforms")
                        .HasForeignKey("PlatformAdministratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlatformAdministrator");
                });

            modelBuilder.Entity("BL.Domain.Project", b =>
                {
                    b.HasOne("BL.Domain.ProjectAdministrator", "ProjectAdministrator")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectAdministratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectAdministrator");
                });

            modelBuilder.Entity("BL.Domain.ProjectAdministrator", b =>
                {
                    b.HasOne("BL.Domain.PlatformAdministrator", "PlatformAdministrator")
                        .WithOne("ProjectAdministrator")
                        .HasForeignKey("BL.Domain.ProjectAdministrator", "PlatformAdministratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlatformAdministrator");
                });

            modelBuilder.Entity("BL.Domain.Question", b =>
                {
                    b.HasOne("BL.Domain.Flow", "Flow")
                        .WithMany("Questions")
                        .HasForeignKey("FlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BL.Domain.Question", null)
                        .WithMany("SubQuestions")
                        .HasForeignKey("QuestionId1");

                    b.Navigation("Flow");
                });

            modelBuilder.Entity("BL.Domain.Respondent", b =>
                {
                    b.HasOne("BL.Domain.Facilitator", "Facilitator")
                        .WithMany("Respondents")
                        .HasForeignKey("FacilitatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BL.Domain.Session", "Session")
                        .WithMany("Respondents")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facilitator");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("BL.Domain.Session", b =>
                {
                    b.HasOne("BL.Domain.Facilitator", "Facilitator")
                        .WithMany("Sessions")
                        .HasForeignKey("FacilitatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facilitator");
                });

            modelBuilder.Entity("PlatformProject", b =>
                {
                    b.HasOne("BL.Domain.Platform", null)
                        .WithMany()
                        .HasForeignKey("PlatformsPlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BL.Domain.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BL.Domain.Facilitator", b =>
                {
                    b.Navigation("Installations");

                    b.Navigation("Notes");

                    b.Navigation("Respondents");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("BL.Domain.Flow", b =>
                {
                    b.Navigation("Contents");

                    b.Navigation("Questions");

                    b.Navigation("SubFlows");
                });

            modelBuilder.Entity("BL.Domain.Installation", b =>
                {
                    b.Navigation("Flows");
                });

            modelBuilder.Entity("BL.Domain.PlatformAdministrator", b =>
                {
                    b.Navigation("Platforms");

                    b.Navigation("ProjectAdministrator");
                });

            modelBuilder.Entity("BL.Domain.Project", b =>
                {
                    b.Navigation("DataAnalyses");

                    b.Navigation("DataAnalysis");

                    b.Navigation("Flows");
                });

            modelBuilder.Entity("BL.Domain.ProjectAdministrator", b =>
                {
                    b.Navigation("DataAnalyses");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("BL.Domain.Question", b =>
                {
                    b.Navigation("Notes");

                    b.Navigation("SubQuestions");
                });

            modelBuilder.Entity("BL.Domain.Respondent", b =>
                {
                    b.Navigation("Feedback");

                    b.Navigation("Installations");
                });

            modelBuilder.Entity("BL.Domain.Session", b =>
                {
                    b.Navigation("Flows");

                    b.Navigation("Respondents");
                });
#pragma warning restore 612, 618
        }
    }
}
