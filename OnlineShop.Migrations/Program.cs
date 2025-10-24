using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineShop.Migrations;

class Program
{
    static void Main()
    {
        string dbHost = GetEnv("POSTGRES_HOST");
        string dbPort = GetEnv("POSTGRES_PORT");
        string dbName = GetEnv("POSTGRES_DB");
        string dbUser = GetEnv("POSTGRES_USER");
        string dbPass = GetEnv("POSTGRES_PASSWORD");

        string connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPass}";

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

    private static string GetEnv(string name)
    {
        string? value = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Environment variable '{name}' is not set.");
        }
        return value;
    }
}