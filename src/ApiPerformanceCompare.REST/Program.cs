using ApiPerformanceCompare.DataModel;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configurationManager = builder.Configuration;
builder.Services.AddDbContext<DataContext>(o =>
{
    o.UseNpgsql(configurationManager.GetConnectionString("DefaultConnection"), b =>
     {
         b.MigrationsAssembly(typeof(DataContext).Assembly.FullName);
         b.SetPostgresVersion(13, 2);
     });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
var context = scope.ServiceProvider.GetRequiredService<DataContext>();
ApplyMigrations(context, logger);
SeeedDatabaseData(context, logger);

app.Run();

static void ApplyMigrations(DataContext context, ILogger logger)
{
    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        logger.LogError($"Migrations apply failed: {ex.Message}", ex);
        throw;
    }
}

static async void SeeedDatabaseData(DataContext context, ILogger logger)
{
    var start = DateTime.Now;
    if (!context.Blogs.Any())
    {
        try
        {
            DataContextSeed.Init(1000);
            await context.Blogs.AddRangeAsync(DataContextSeed.Blogs);
            await context.Posts.AddRangeAsync(DataContextSeed.Posts);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"Data generation method failed: {ex.Message}", ex);
            throw;
        }
    }

    logger.LogInformation("База данных актуализированна за " + (DateTime.Now - start));
}
