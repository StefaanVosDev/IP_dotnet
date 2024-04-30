using BL.Domain;
using BL.Domain.Answers;
using BL.Domain.Questions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Google.Cloud.SecretManager.V1;
using Npgsql;

namespace DAL.EF;

public class PhygitalDbContext : IdentityDbContext<IdentityUser>
{
    #region Constructors
    
    public PhygitalDbContext()
    {
        //TODO fix this for migrations
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "service-acc-key.json");
    }

    public PhygitalDbContext(DbContextOptions<PhygitalDbContext> options) : base(options)
    {   
        
    }

    #endregion

    #region vars

    public DbSet<Answer> Answers { get; set; }
    public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
    public DbSet<OpenQuestion> OpenQuestions { get; set; }
    public DbSet<RangeQuestion> RangeQuestions { get; set; }
    public DbSet<SingleChoiceQuestion> SingleChoiceQuestions { get; set; }
    public DbSet<Flow> Flows { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Session> Sessions { get; set; }

    #endregion

    #region On... override methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (optionsBuilder.IsConfigured) return;

        var connectionString = $"Host=35.240.22.60;Database=postgres;Username=postgres;Password={AccessSecret("db_password")};";
        const string localConnection = "Host=localhost;Database=postgres;Username=postgres;Password=password123;";
        try
        {
            optionsBuilder.UseNpgsql(connectionString);
            var context = new NpgsqlConnection(connectionString);
            context.Open();
        }
        catch (NpgsqlException)
        {
            try
            {
                optionsBuilder.UseNpgsql(localConnection);
            }
            catch (NpgsqlException)
            {
                Console.WriteLine("local database not available. Check connection string in appsettings.json.");
            }
        }
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
        if (!wipeDatabase) return false;
        EmptyDatabase();
        return true;
    }

    private void EmptyDatabase()
    {
        // Get all the DbSets properties
        var dbSets = GetType().GetProperties().Where(p =>
            p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

        foreach (var dbSet in dbSets)
        {
            // Get the DbSet instance
            var dbSetInstance = dbSet.GetValue(this);
            if (dbSetInstance == null) continue;
            // Get the RemoveRange method
            var removeRangeMethod = typeof(DbContext)
                .GetMethod(nameof(DbContext.RemoveRange), new Type[] { typeof(IEnumerable<>) })
                ?.MakeGenericMethod(dbSet.PropertyType.GetGenericArguments());

            // Invoke the RemoveRange method with the DbSet instance
            if (removeRangeMethod != null)
            {
                removeRangeMethod.Invoke(this, new object[] { dbSetInstance });
            }
        }

        SaveChanges();
    }

    #endregion
    
    #region AccessSecret
    private string AccessSecret(string secretId)
    {
        // Create the Secret Manager client.
        SecretManagerServiceClient client = SecretManagerServiceClient.Create();

        // Build the resource name of the secret version.
        SecretVersionName secretVersionName = new SecretVersionName("269636205630", secretId, "latest");

        // Access the secret version.
        AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);

        // Get the secret payload and decode it.
        string payload = result.Payload.Data.ToStringUtf8();

        return payload;
    }
    #endregion
    
    //TODO: run database migrations.
    //TODO:ProjectDirectory>cd DAL
    //TODO:ProjectDirectory\DAL>dotnet ef migrations add {{migrationName}}
    //If dotnet ef is not installed:
    //ProjectDirectory>dotnet tool install --global dotnet-ef (--version {{version}})
}