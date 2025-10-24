using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineShop.Data;

public static class MigrationExtensions
{
    public static void MigrateDatabase(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}