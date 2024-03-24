using Microsoft.Extensions.DependencyInjection;
using MoscowWeatherApp.Core.Services;
using MoscowWeatherApp.Domain.Interfaces;
using MoscowWeatherApp.Domain.Models;

namespace MoscowWeatherApp.Core.Extensions;

/// <summary>
/// Расширение для регистрации сервисов.
/// </summary>
public static class ServicesRegistrationExtensions
{
    /// <summary>
    /// Метод регистрации сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов с зарегистрированными зависимостями.</returns>
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddTransient<IWeatherService<WeatherInfo, WeatherFilters>, WeatherService>();
        services.AddTransient<IExcelService<WeatherInfo, ExcelParameters>, WeatherExcelSerivce>();

        return services;
    }
}
