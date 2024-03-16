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
    
    //DbSet<>
    
    #endregion

    #region On... override methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (optionsBuilder.IsConfigured) return;
        const string connectionString = "Host=35.240.22.60;Database=postgres;Username=postgres;Password=%^c~JK,s-H^1}sde;";
        optionsBuilder.UseNpgsql(connectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Flow>()
            .HasMany(s => s.SubFlows)
            .WithOne(s => s.ParentFlow)
            .HasForeignKey(s => s.ParentFlowId);
    }

    #endregion
     
    //TODO: run database migrations.
    //TODO:ProjectDirectory>cd DAL
    //TODO:ProjectDirectory\DAL>dotnet ef migrations add {{migrationName}}
    //If dotnet ef is not installed:
    //ProjectDirectory>dotnet tool install --global dotnet-ef (--version {{version}})
}