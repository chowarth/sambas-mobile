using System.Text.Json;
using System.Text.Json.Serialization;
using CommunityToolkit.Maui;
using IconFont.Maui.FluentIcons;
using Microsoft.Extensions.Logging;
using Sambas.Mobile.Features.Matches;
using Sambas.Mobile.Features.Startup;
using Sambas.Mobile.Features.Tournaments;
using Serilog;
using Serilog.Templates;
using Shiny;
using Shiny.DocumentDb.Sqlite;
using UXDivers.Popups.Maui;

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
                .Add<MatchListPage, MatchListPageViewModel>(registerRoute: false)
                .Add<TournamentListPage, TournamentListPageViewModel>(registerRoute: false)
                .Add<TournamentDetailsPage, TournamentDetailsPageViewModel>()
            )
            .UseMauiCommunityToolkit()
            .UseUXDiversPopups()
            .UseFluentIconsFilled()
            .UseFluentIconsRegular()
            .CustomiseHandlers()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services
            .AddPopupRegistrations()
            .AddLogging(builder =>
                builder.ConfigureSerilog()
            )
            .AddSqliteDocumentStore(options =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Sambas.db");
                options.DatabaseProvider = new SqliteDatabaseProvider($"Data Source={dbPath}");
                //options.MapTypeToTable<Models.Team>("teams", t => t.Id);
                //options.MapTypeToTable<Models.Match>("matches", t => t.Id);

                var context = new AppJsonContext(new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
                options.JsonSerializerOptions = context.Options;
                options.UseReflectionFallback = false; // Recommended for AOT
            });

        // Register this AppShell so that it can be used for navigation purposes by Shiny's INavigator.
        // It will be resolved when used with navigator.SwitchShell<AppShell>().
        builder.Services.AddTransient<AppShell>();

        return builder.Build();
    }

    private static IServiceCollection AddPopupRegistrations(this IServiceCollection services)
    {
        // Explicitly call the extension method AddTransientPopup because the same method
        // exists in both UXDivers.Popups.Maui and CommunityToolkit.Maui.
        UXDivers.Popups.Maui.ServiceCollectionExtensions
            .AddTransientPopup<EditMatchDetailsPopup, EditMatchDetailsPopupViewModel>(services);
        UXDivers.Popups.Maui.ServiceCollectionExtensions
            .AddTransientPopup<EditTournamentDetailsPopup, EditTournamentDetailsPopupViewModel>(services);

        return services;
    }

    private static MauiAppBuilder CustomiseHandlers(this MauiAppBuilder builder)
    {
#if ANDROID
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("SambasEntryCustomisation", (handler, view) =>
        {
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            handler.PlatformView.SetPadding(0, 0, 0, 0);
        });

        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("SambasEntryCustomisation", (handler, view) =>
        {
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            handler.PlatformView.SetPadding(0, 0, 0, 0);
        });

        Microsoft.Maui.Handlers.TimePickerHandler.Mapper.AppendToMapping("SambasEntryCustomisation", (handler, view) =>
        {
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            handler.PlatformView.SetPadding(0, 0, 0, 0);
        });
#endif
        return builder;
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

// 'This operation requires a JsonTypeInfo<Match>. Use the Query<T>(JsonTypeInfo<T>) overload.'
// If AppJsonContext is used then there is no error, the docs don't mention anything about it for the default
// set up, so why is it required in this case? Is it because of the use of Query or OrderByDescending?
// I need to understand the underlying reason for this error and how the AppJsonContext resolves it.

// See: https://shinylib.net/data/documentdb/aot/#_top
[JsonSerializable(typeof(Models.Match))]
[JsonSerializable(typeof(Models.Tournament))]
public partial class AppJsonContext : JsonSerializerContext;
