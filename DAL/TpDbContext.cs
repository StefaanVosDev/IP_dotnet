using BL.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

//TODO: Change the name of the DBContext for the project
public class TpDbContext : DbContext
{
    #region Constructors

    public TpDbContext()
    {
    }

    //TODO: Change the name of the Generic to match the DbContext name
    public TpDbContext(DbContextOptions<TpDbContext> options) : base(options)
    {
    }

    #endregion

    //TODO: Add the classes needed in the DB
    #region vars

    //TODO: Change the Generic to a class being used 
    //public DbSet<Template> Templates { get; set; }

    #endregion

    #region On... override methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            //TODO: Change the connection string to the correct string for the DB
            //Template: "server={{ip address}};database={{database to modify}};user={{user}};password={{user's password}}"
            var connectionString =
                "jdbc:postgresql://35.240.22.60/postgres?password=<Dsvok:]C.\\hO0I";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }

    //TODO: setup OnModelCreating to match the current project needs.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    #endregion
    
    //TODO: run database migrations.
    //TODO:ProjectDirectory>cd DAL
    //TODO:ProjectDirectory\DAL>dotnet ef migrations add {{migrationName}}
    //If dotnet ef is not installed:
    //ProjectDirectory>dotnet tool install --global dotnet-ef (--version {{version}})
}