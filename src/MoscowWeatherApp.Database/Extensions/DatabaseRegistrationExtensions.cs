using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoscowWeatherApp.Database.Data;
using MoscowWeatherApp.Database.Repositories;
using MoscowWeatherApp.Domain.Constants;

namespace MoscowWeatherApp.Database.Extensions;

/// <summary>
/// Расширение для регистрации зависимостей БД.
/// </summary>
public static class DatabaseRegistrationExtensions
{
    /// <summary>
    /// Добавить сервисы БД.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="connectionString">Строка подключения.</param>
    /// <returns>Коллекция сервисов с зарегистрированными зависимостями.</returns>
    public static IServiceCollection AddDbServices(
        this IServiceCollection services,
        string? connectionString,
        Action<DbContextOptionsBuilder>? configureAction = null)
    {
        services.AddDbContext<MoscowWeatherAppDbContext>(builder =>
        {
            builder.UseNpgsql(
                connectionString,
                b => b.MigrationsHistoryTable(DatabaseConstants.MigrationsTableName, DatabaseConstants.Schema));
            configureAction?.Invoke(builder);
        });

        services.AddScoped<WeatherRepository>();

        return services;
    }
}
