using ApiPerformanceCompare.DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiPerformanceCompare.DataModel;

public class DataContext : DbContext
{
    protected readonly DbContextOptions<DataContext> options;

    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
        this.options = options;
    }

    public DbSet<Blog>? Blogs { get; set; }
    public DbSet<Post>? Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
