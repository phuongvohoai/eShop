using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Phuong.eShop.ServiceDefaults.Migration;

internal class MigrationHostedService<TContext>(IServiceProvider serviceProvider, bool hasDbSeeder = false)
    : BackgroundService where TContext : DbContext
{
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        var seeder = hasDbSeeder ? scope.ServiceProvider.GetRequiredService<IDbSeeder<TContext>>() : null;
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);

            logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await context.Database.MigrateAsync(cancellationToken);
                if (seeder is not null)
                {
                    await seeder.SeedAsync(context, cancellationToken);
                }
            });

            logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}