using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ApiPerformanceCompare.DataModel
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>()
                .UseNpgsql("DefaultConnection", b =>
                {
                    b.MigrationsAssembly(typeof(DataContext).Assembly.FullName);
                    b.SetPostgresVersion(13, 2);
                });
            return new DataContext(optionsBuilder.Options);
        }
    }
}
