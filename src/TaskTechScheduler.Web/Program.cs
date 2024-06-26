using Contracts;
using Contracts.Repositories;
using Services;
using Services.Repositories;
using TaskTechScheduler.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IReportMaker, ReportMaker>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.AddControllersWithViews();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
