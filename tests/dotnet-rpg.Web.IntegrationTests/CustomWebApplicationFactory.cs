using dotnet_rpg.Infrastructure.Persistence;
using dotnet_rpg.Web.IntegrationTests.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_rpg.Web.IntegrationTests;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>

{
    private EfTestDbInitializer _dbInitializer;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ApplicationDbContext>));

            services.Remove(descriptor);
            
            services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseSqlite("Data Source=dotnet-rpgDB-IntegrationTests.db");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();

            try
            {
                _dbInitializer = new EfTestDbInitializer(dbContext);
                _dbInitializer.InitializeDb();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (_dbInitializer != null)
        {
            _dbInitializer.CleanUp();
        }
        base.Dispose(disposing);
    }
}