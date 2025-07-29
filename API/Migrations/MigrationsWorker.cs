using System.Reflection;
using DbUp;

namespace API.Migrations;

public static class MigrationsWorker
{
    public static (int, Exception) ApplyMigrations(string connectionString)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var migrationsDirectory = Path.Combine(currentDirectory, "Migrations");
        
        var databaseUpgradeResult = DeployChanges.To
            .SqlDatabase(connectionString)
            .JournalToSqlTable("dbo", "_migrations")
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), (string s) => s.Equals(migrationsDirectory))
            .WithScriptsFromFileSystem(migrationsDirectory)
            .WithExecutionTimeout(new TimeSpan(0, 0, 90))
            .WithTransactionPerScript()
            .LogToConsole()
            .Build()
            .PerformUpgrade();

        if (!databaseUpgradeResult.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(databaseUpgradeResult.Error);
            Console.ResetColor();
            return (-1, databaseUpgradeResult.Error);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return (0, null)!;
    }
}