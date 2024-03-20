using BL.Domain;
using BL.Domain.Answers;
using BL.Domain.Questions;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

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

    private DbSet<MultipleChoiceAnswer> MultipleChoiceAnswers { get; set; }
    private DbSet<OpenAnswer> OpenAnswers { get; set; }
    private DbSet<RangeAnswer> RangeAnswers { get; set; }
    private DbSet<SingleChoiceAnswer> SingleChoiceAnswers { get; set; }
    private DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
    private DbSet<OpenQuestion> OpenQuestions { get; set; }
    private DbSet<RangeQuestion> RangeQuestions { get; set; }
    private DbSet<SingleChoiceQuestion> SingleChoiceQuestions { get; set; }
    private DbSet<Flow> Flows { get; set; }
    private DbSet<Project> Projects { get; set; }
    
    
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
    
    public bool CreateDatabase(bool wipeDatabase = true)
    {
        if (wipeDatabase)
        {
            EmptyDatabase();
        }

        return Database.EnsureCreated();
    }
    
    public void EmptyDatabase()
    {
        // Get all the DbSets properties
        var dbSets = GetType().GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

        foreach (var dbSet in dbSets)
        {
            // Get the generic method for the specific DbSet
            var method = typeof(PhygitalDbContext).GetMethod("ClearTable").MakeGenericMethod(dbSet.PropertyType.GetGenericArguments());

            // Invoke the method
            method.Invoke(this, new object[] { dbSet.GetValue(this) });
        }

        SaveChanges();
    }

    public void ClearTable<T>(DbSet<T> dbSet) where T : class
    {
        dbSet.RemoveRange(dbSet);
    }
    #endregion
     
    
    //TODO: run database migrations.
    //TODO:ProjectDirectory>cd DAL
    //TODO:ProjectDirectory\DAL>dotnet ef migrations add {{migrationName}}
    //If dotnet ef is not installed:
    //ProjectDirectory>dotnet tool install --global dotnet-ef (--version {{version}})
}