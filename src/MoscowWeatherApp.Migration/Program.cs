using MoscowWeatherApp.Migration.Extensions;
using Serilog;

namespace MoscowWeatherApp.Migration;

public class Program
{
    public static void Main(string[ ] args)
    {
        var configurationBuilder = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var configuration = configurationBuilder.Build();

        Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(configuration)
          .CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddDependencies();

            var app = builder.Build();
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}