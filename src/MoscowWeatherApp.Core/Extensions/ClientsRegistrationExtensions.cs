using Microsoft.Extensions.DependencyInjection;

namespace MoscowWeatherApp.Core.Extensions;

/// <summary>
/// Расширение для регистрации клиентов.
/// </summary>
public static class ClientsRegistrationExtensions
{
    /// <summary>
    /// Метод добавления клиентов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов с зарегистрированными зависимостями.</returns>
    public static IServiceCollection AddClients(
        this IServiceCollection services)
    {
        return services;
    }
}
