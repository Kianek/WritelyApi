using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using WritelyApi.Data;
using WritelyApi.IntegrationTests.Helpers;
using Xunit;

namespace WritelyApi.IntegrationTests
{
    public class CustomWebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbServiceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("WritelyInMemoryDb");
                    options.UseInternalServiceProvider(dbServiceProvider);
                });

                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebAppFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        Utilities.SeedDatabase(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error creating the database. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}
