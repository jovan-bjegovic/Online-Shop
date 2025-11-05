using DotNetEnv;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineShop.Migrations;

internal class Program
{
    private static void Main(string[] args)
    {
        Env.Load();
        
        if (args.Length < 5)
        {
            Console.WriteLine("Usage: dotnet OnlineShop.Migrations.dll <host> <port> <db> <user> <password>");
            
            return;
        }

        string host = args[0];
        string port = args[1];
        string db = args[2];
        string user = args[3];
        string password = args[4];

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