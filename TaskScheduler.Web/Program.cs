using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile("appsettings.json");

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite(connectionString,b => b.MigrationsAssembly("TaskScheduler.Web"))
);

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

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
    pattern: "{controller=Auth}/{action=Index}");

app.Run();
