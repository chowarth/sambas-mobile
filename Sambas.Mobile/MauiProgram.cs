using Microsoft.Extensions.Logging;
using Sambas.Mobile.SelectTeam;
using Serilog;
using Serilog.Templates;
using Shiny;

namespace Sambas.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseShinyShell(x => x
                .Add<SelectTeamPage, SelectTeamPageViewModel>(registerRoute: false)
            )
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddLogging(builder =>
            builder.ConfigureSerilog()
        );

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
