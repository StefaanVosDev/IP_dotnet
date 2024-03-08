using BL;
using BL.Domain;
using DAL;
using Microsoft.EntityFrameworkCore;

#region Builder services

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//TODO: change the generic DbContext to the correct DbContext present in the project.
builder.Services.AddDbContext<TpDbContext>(options =>
{
    //TODO: change the connectionString name to the correct connectionString name found in "appsettings.json"
    //TODO: check line 8 for this in the appsettings.json
    var connectionString = builder.Configuration.GetConnectionString("TemplatesConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

#region AddScoped
//TODO: check if the Repository is still ok.
builder.Services.AddScoped<IRepository, Repository>();
//TODO: Change the IManager and Manager generic type to the correct Domain models and add scopes when needed.
//builder.Services.AddScoped<IManager<Template>, Manager<Template>>();

#endregion

#endregion

#region App services

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

//TODO: Change the default controller route if not using Home as default.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion