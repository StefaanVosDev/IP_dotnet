using BL;
using BL.Domain;
using BL.Domain.Questions;
using BL.Implementations;
using BL.Interfaces;
using DAL;
using DAL.EF;
using DAL.Implementations;
using DAL.Interfaces;
using IP_MVC;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PhygitalDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Connection");
    options.UseNpgsql(connectionString);
});

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
        SeedIdentity(userManager, roleManager);
        
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedIdentity(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    var userRole = new IdentityRole
    {
        Name = CustomIdentityConstants.UserRole
    };
    roleManager.CreateAsync(userRole).Wait();
    var adminRole = new IdentityRole
    {
        Name = CustomIdentityConstants.AdminRole
    };
    roleManager.CreateAsync(adminRole).Wait();

    var user = new IdentityUser
    {
        Email = "admin@kdg.be",
        UserName = "admin@kdg.be",
        EmailConfirmed = true
    };
    userManager.CreateAsync(user, "Password1!").Wait();

    userManager.AddToRoleAsync(user, CustomIdentityConstants.AdminRole);
}

public partial class Program
{
}