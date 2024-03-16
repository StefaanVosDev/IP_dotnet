using BL;
using BL.Domain;
using BL.Domain.Questions;
using DAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//TODO remove this when deploying to production
try
{
    builder.Services.AddDbContext<PhygitalDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("Connection");
        options.UseNpgsql(connectionString);
    });
}
catch
{
    // If connection to the database fails, use InMemoryRepository
    builder.Services.AddSingleton<IRepository, InMemoryRepository>();
}

// Add Managers as a transient service
builder.Services.AddTransient<Manager<Project>>();
builder.Services.AddTransient<Manager<Question>>();
builder.Services.AddTransient<Manager<Flow>>();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed data into the InMemoryRepository if it was used
if (app.Services.GetService<IRepository>() is InMemoryRepository)
{
    var dbContext = app.Services.GetService<PhygitalDbContext>();
    InMemoryRepository.Seed(dbContext);
}

app.Run();