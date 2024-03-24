using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MoscowWeatherApp.Settings.Extensions;

/// <summary>
/// Расширение для регистрации настроек из конфига.
/// </summary>
public static class SettingsRegistrationExtensions
{
    /// <summary>
    /// Доабвление настроек из конфига.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <returns>Коллекция сервисов с зарегистрированными зависимостями.</returns>
    public static IServiceCollection AddSettings(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        return services;
    }
}
