using Microsoft.EntityFrameworkCore;
using Models.Configurations;

namespace Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserAdminsConfiguration());
    }

    public DbSet<Tasks> Tasks{get;set;}
    public DbSet<UserAdmins> Users{get;set;}
}