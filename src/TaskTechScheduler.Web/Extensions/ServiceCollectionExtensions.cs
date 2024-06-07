using Microsoft.EntityFrameworkCore;
using Models;

namespace TaskTechScheduler.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config)
    {

        services.AddDbContext<AppDbContext>(
            opt => opt.UseSqlite(config.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("TaskTechScheduler.Web"))
        );
    }
}