using MoscowWeatherApp.Core.Extensions;
using MoscowWeatherApp.Server.Extensions;

namespace MoscowWeatherApp.Server;

public class Program
{
    public static void Main(string[ ] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddDependencies();

        builder.AddAutoMappers();

        var app = builder.Build();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });

        app.Run();
    }
}
