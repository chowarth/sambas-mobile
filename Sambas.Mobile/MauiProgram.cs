using Microsoft.Extensions.Logging;
using Sambas.Mobile.Features.Startup;
using Sambas.Mobile.Models;
using Serilog;
using Serilog.Templates;
using Shiny;
using Shiny.DocumentDb.Sqlite;

namespace Sambas.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseShinyShell(x => x
                .Add<StartupPage, StartupPageViewModel>(registerRoute: false)
            )
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddLogging(builder =>
            builder.ConfigureSerilog()
        )
        .AddSqliteDocumentStore(options =>
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Sambas.db");

            options.DatabaseProvider = new SqliteDatabaseProvider($"Data Source={dbPath}");
            options.MapTypeToTable<Team>("teams", t => t.Id);
        });

        // Register this AppShell so that it can be used for navigation purposes by Shiny's INavigator.
        // It will be resolved when used with navigator.SwitchShell<AppShell>().
        builder.Services.AddTransient<AppShell>();

        return builder.Build();
    }

    private static void ConfigureSerilog(this ILoggingBuilder builder)
    {
        var template = new ExpressionTemplate(
            "[{@t:yyyy-MM-dd HH:mm:ss.fff zzz}] " +
            "[{@l:u3}]" +
            "{#if SourceContext is not null} [{Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)}]{#end}" +
            " {@m}\n{@x}");

        var logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
            .WriteTo.Debug(template)
#endif
            // Add additional sinks here
            .CreateLogger();

        builder.AddSerilog(logger);
    }
}
