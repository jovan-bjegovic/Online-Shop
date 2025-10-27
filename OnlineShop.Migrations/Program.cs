using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineShop.Migrations;

internal class Program
{
    private static void Main()
    {
        string? host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
        string? port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
        string? db = Environment.GetEnvironmentVariable("POSTGRES_DB");
        string? user = Environment.GetEnvironmentVariable("POSTGRES_USER");
        string? password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port) ||
            string.IsNullOrEmpty(db) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("ERROR: Postgres environment variables not set.");
            return;
        }

        string connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={password}";
        
        var serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();

        Console.WriteLine("Migrations completed successfully.");
    }
}