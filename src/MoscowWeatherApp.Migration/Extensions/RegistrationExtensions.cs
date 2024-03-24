using MoscowWeatherApp.Database;
using MoscowWeatherApp.Domain.Constants;
using MoscowWeatherApp.Domain.Interfaces;
using MoscowWeatherApp.Database.Extensions;

namespace MoscowWeatherApp.Migration.Extensions;

/// <summary>
/// Расширение для регистрации зависимостей.
/// </summary>
public static class RegistrationExtensions
{
    /// <summary>
    /// Общее добавление всех зависимостей в builder.
    /// </summary>
    public static IHostApplicationBuilder AddDependencies(
        this IHostApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables();

        builder.Services.AddDbServices(builder.Configuration.GetConnectionString(DatabaseConstants.ConnectionStringName));
        builder.Services.AddSingleton<IMigrator, Migrator>();
        builder.Services.AddHostedService<MigrationHostedService>();

        return builder;
    }
}