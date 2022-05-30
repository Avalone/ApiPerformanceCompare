using ApiPerformanceCompare.DataModel;
using ApiPerformanceCompare.GraphQL.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configurationManager = builder.Configuration;

builder.Services.AddDbContext<DataContext>(o =>
{
    o.UseNpgsql(configurationManager.GetConnectionString("DefaultConnection"), b =>
    {
        b.MigrationsAssembly(typeof(DataContext).Assembly.FullName);
        b.SetPostgresVersion(13, 2);
    });
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddFileSystemQueryStorage("./PersistedQueries")
    .UsePersistedQueryPipeline();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();
//app.UseCors();

app.UseWebSockets();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("/graphql", true);
        return Task.CompletedTask;
    });
});

app.Run();
