using dotnet_rpg.Infrastructure.Persistence;
using dotnet_rpg.Web.IntegrationTests.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_rpg.Web.IntegrationTests;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>

{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);
            
            
            services.AddScoped()
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite("Data Source=dotnet-rpgDBTest.db");
            });
            
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        });
    }
}