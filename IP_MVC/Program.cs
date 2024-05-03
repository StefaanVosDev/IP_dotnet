using BL;
using BL.Domain;
using BL.Domain.Questions;
using BL.Implementations;
using BL.Interfaces;
using DAL;
using DAL.EF;
using DAL.Implementations;
using DAL.Interfaces;
using Google.Cloud.SecretManager.V1;
using IP_MVC;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PhygitalDbContext>(options =>
{
    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "service-acc-key.json");
    try
    {
        var connectionString = builder.Configuration.GetConnectionString("Connection") + AccessSecret("db_password") + ";";
        var testConnection = new NpgsqlConnection(connectionString);
        testConnection.Open();
        testConnection.Close();
        options.UseNpgsql(connectionString);
    }
    catch (NpgsqlException)
    {
        Console.WriteLine("Google Cloud database not available. Trying local database.");
        try
        {
            var localConnectionString = builder.Configuration.GetConnectionString("LocalConnection");
            var localTestConnection = new NpgsqlConnection(localConnectionString);
            localTestConnection.Open();
            localTestConnection.Close();
            options.UseNpgsql(localConnectionString);
        }
        catch (Exception e)
        {
            Console.WriteLine("No valid database available. Check database or connection string in appsettings.json.");
            Console.WriteLine(e.Message);
            Environment.Exit(1);
        }
    }
});

// Add Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PhygitalDbContext>();

// Add session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add scoped
builder.Services.AddScoped<IRepository,Repository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IFlowRepository, FlowRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<Manager<Question>>();
builder.Services.AddScoped<IProjectManager, ProjectManager>();
builder.Services.AddScoped<IQuestionManager, QuestionManager>();
builder.Services.AddScoped<ISessionManager, SessionManager>();
builder.Services.AddScoped<IFlowManager, FlowManager>();
builder.Services.AddScoped<ICloudManager, CloudManager>();
builder.Services.AddScoped<ICloudStorageRepository, CloudStorageRepository>();
builder.Services.AddScoped<IAnswerManager, AnswerManager>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();



// Add authorization
builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.Events.OnRedirectToLogin += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments("/api"))
        {
            ctx.Response.StatusCode = 401;
        }

        return Task.CompletedTask;
    };

    cfg.Events.OnRedirectToAccessDenied += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments("/api"))
        {
            ctx.Response.StatusCode = 403;
        }

        return Task.CompletedTask;
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PhygitalDbContext>();
    if (context.CreateDatabase(false))
    {
        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();
        await SeedIdentity(userManager, roleManager);
        
        DataSeeder.Seed(context);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{parentFlowId?}");

app.MapRazorPages();

app.Run();
return;

async Task SeedIdentity(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    var userRole = new IdentityRole
    {
        Name = CustomIdentityConstants.FacilitatorRole
    };
    await roleManager.CreateAsync(userRole);
    var adminRole = new IdentityRole
    {
        Name = CustomIdentityConstants.AdminRole
    };
    await roleManager.CreateAsync(adminRole);

    var adminUser = new IdentityUser
    {
        Email = "admin@kdg.be",
        UserName = "admin@kdg.be",
        EmailConfirmed = true
    };
    await userManager.CreateAsync(adminUser, "Password1!");
    await userManager.AddToRoleAsync(adminUser, CustomIdentityConstants.AdminRole);

    var facilitatorUser = new IdentityUser
    {
        Email = "fac@kdg.be",
        UserName = "fac@kdg.be",
        EmailConfirmed = true
    };
    await userManager.CreateAsync(facilitatorUser, "Password1!");
    await userManager.AddToRoleAsync(facilitatorUser, CustomIdentityConstants.FacilitatorRole);
}

string AccessSecret(string secretId)
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

//Do not delete
public partial class Program
{
}