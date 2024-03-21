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
        
        modelBuilder.Entity<RangeQuestion>()
            .Property(r => r.Min)
            .IsRequired();

        modelBuilder.Entity<RangeQuestion>()
            .Property(r => r.Max)
            .IsRequired();
    }
    
    public bool CreateDatabase(bool wipeDatabase = true)
    {
        if (!wipeDatabase) return false;
        EmptyDatabase();
        return true;
    }

    private void EmptyDatabase()
    {
        // Get all the DbSets properties
        var dbSets = GetType().GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

        foreach (var dbSet in dbSets)
        {
            // Get the DbSet instance
            var dbSetInstance = dbSet.GetValue(this);
            if (dbSetInstance != null)
            {
                // Get the RemoveRange method
                var removeRangeMethod = typeof(DbContext).GetMethod(nameof(DbContext.RemoveRange), new Type[] { typeof(IEnumerable<>) })?.MakeGenericMethod(dbSet.PropertyType.GetGenericArguments());

                // Invoke the RemoveRange method with the DbSet instance
                if (removeRangeMethod != null)
                {
                    removeRangeMethod.Invoke(this, new object[] { dbSetInstance });
                }
            }
        }

        SaveChanges();
    }
    #endregion
     
    
    //TODO: run database migrations.
    //TODO:ProjectDirectory>cd DAL
    //TODO:ProjectDirectory\DAL>dotnet ef migrations add {{migrationName}}
    //If dotnet ef is not installed:
    //ProjectDirectory>dotnet tool install --global dotnet-ef (--version {{version}})
}