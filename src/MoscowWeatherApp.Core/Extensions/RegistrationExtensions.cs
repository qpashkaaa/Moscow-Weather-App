using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoscowWeatherApp.Database.Extensions;
using MoscowWeatherApp.Domain.Constants;
using MoscowWeatherApp.Settings.Extensions;

namespace MoscowWeatherApp.Core.Extensions;

/// <summary>
/// Расширение для регистрации зависимостей.
/// </summary>
public static class RegistrationExtensions
{
    /// <summary>
    /// Метод добавления всех зависимостей в builder.
    /// </summary>
    /// <returns>Билдер с зарегистрированными зависимостями.</returns>
    public static IHostApplicationBuilder AddDependencies(
        this IHostApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables();

        builder.Services.AddControllersWithViews();
        builder.Services.AddSettings(builder.Configuration);
        builder.Services.AddDbServices(builder.Configuration.GetConnectionString(DatabaseConstants.ConnectionStringName));
        builder.Services.AddClients();
        builder.Services.AddServices();

        return builder;
    }
}
