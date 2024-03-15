using BL.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class PhygitalDbContext : DbContext
{
    #region Constructors

    public PhygitalDbContext()
    {
    }
    
    public PhygitalDbContext(DbContextOptions<PhygitalDbContext> options) : base(options)
    {
    }

    #endregion

    //TODO: Add the users needed in the DB
    #region vars
    
    public DbSet<Content> Contents { get; set; }
    public DbSet<DataAnalysis> DataAnalyses { get; set; }
    public DbSet<Facilitator> Facilitators { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Flow> Flows { get; set; }
    public DbSet<Installation> Installations { get; set; }
    public DbSet<Notes> Notes { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Respondent> Respondents { get; set; }
    public DbSet<Session> Sessions { get; set; }
    
    #endregion

    #region On... override methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString =
                "Host=35.240.22.60;Database=postgres;Username=postgres;Password=%^c~JK,s-H^1}sde;";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Facilitator>()
            .HasMany(f => f.Installations)
            .WithOne(i => i.Facilitator)
            .HasForeignKey(i => i.FacilitatorId);

        modelBuilder.Entity<Session>()
            .HasMany(s => s.Flows)
            .WithOne(f => f.Session)
            .HasForeignKey(f => f.SessionId);

        modelBuilder.Entity<Flow>()
            .HasMany(f => f.Contents)
            .WithOne(c => c.Flow)
            .HasForeignKey(c => c.FlowId);

        modelBuilder.Entity<Installation>()
            .HasMany(i => i.Flows)
            .WithOne(f => f.Installation)
            .HasForeignKey(f => f.InstallationId);

        modelBuilder.Entity<Facilitator>()
            .HasMany(f => f.Notes)
            .WithOne(n => n.Facilitator)
            .HasForeignKey(n => n.FacilitatorId);

        modelBuilder.Entity<Facilitator>()
            .HasMany(f => f.Respondents)
            .WithOne(r => r.Facilitator)
            .HasForeignKey(r => r.FacilitatorId);

        modelBuilder.Entity<Project>()
            .HasMany(p => p.Flows)
            .WithOne(f => f.Project)
            .HasForeignKey(f => f.ProjectId);

        modelBuilder.Entity<DataAnalysis>()
            .HasOne(d => d.Project)
            .WithMany(p => p.DataAnalyses)
            .HasForeignKey(d => d.ProjectId);

        modelBuilder.Entity<Respondent>()
            .HasMany(r => r.Installations)
            .WithOne(i => i.Respondent)
            .HasForeignKey(i => i.RespondentId);

        modelBuilder.Entity<PlatformAdministrator>()
            .HasOne(pa => pa.ProjectAdministrator)
            .WithOne(pa => pa.PlatformAdministrator)
            .HasForeignKey<ProjectAdministrator>(pa => pa.PlatformAdministratorId);

        modelBuilder.Entity<PlatformAdministrator>()
            .HasMany(pa => pa.Platforms)
            .WithOne(p => p.PlatformAdministrator)
            .HasForeignKey(p => p.PlatformAdministratorId);

        modelBuilder.Entity<ProjectAdministrator>()
            .HasMany(pa => pa.DataAnalyses)
            .WithOne(d => d.ProjectAdministrator)
            .HasForeignKey(d => d.ProjectAdministratorId);

        modelBuilder.Entity<ProjectAdministrator>()
            .HasMany(pa => pa.Projects)
            .WithOne(p => p.ProjectAdministrator)
            .HasForeignKey(p => p.ProjectAdministratorId);

        modelBuilder.Entity<Facilitator>()
            .HasMany(f => f.Sessions)
            .WithOne(s => s.Facilitator)
            .HasForeignKey(s => s.FacilitatorId);

        modelBuilder.Entity<Session>()
            .HasMany(s => s.Respondents)
            .WithOne(r => r.Session)
            .HasForeignKey(r => r.SessionId);

        modelBuilder.Entity<Respondent>()
            .HasMany(r => r.Feedback)
            .WithOne(f => f.Respondent)
            .HasForeignKey(f => f.RespondentId);

        modelBuilder.Entity<Question>()
            .HasMany(q => q.Notes)
            .WithOne(n => n.Question)
            .HasForeignKey(n => n.NoteId);

        modelBuilder.Entity<Project>()
            .HasMany(p => p.Platforms)
            .WithMany(pl => pl.Projects);

        modelBuilder.Entity<Flow>()
            .HasMany(f => f.Questions)
            .WithOne(q => q.Flow)
            .HasForeignKey(q => q.FlowId);
    }

    #endregion
     
    //TODO: run database migrations.
    //TODO:ProjectDirectory>cd DAL
    //TODO:ProjectDirectory\DAL>dotnet ef migrations add {{migrationName}}
    //If dotnet ef is not installed:
    //ProjectDirectory>dotnet tool install --global dotnet-ef (--version {{version}})
}