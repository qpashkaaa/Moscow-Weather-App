using Microsoft.Extensions.Logging;
using MoscowWeatherApp.Database.Repositories;
using MoscowWeatherApp.Domain.Interfaces;
using MoscowWeatherApp.Domain.Models;

namespace MoscowWeatherApp.Core.Services;

/// <summary>
/// Сервис для работы с ифнормацией о погоде.
/// </summary>
public class WeatherService : IWeatherService<WeatherInfo, WeatherFilters>
{
    /// <summary>
    /// Репозиторий для работы с сущностями <see cref="WeatherInfo"/>.
    /// </summary>
    private readonly WeatherRepository _weatherRepository;

    /// <summary>
    /// Логер.
    /// </summary>
    private readonly ILogger<WeatherService> _logger;

    /// <summary>
    /// Создание <see cref="WeatherService"/>.
    /// </summary>
    /// <param name="weatherRepository">Репозиторий для работы с сущностями <see cref="WeatherInfo"/>.</param>
    /// <param name="logger">Логер.</param>
    public WeatherService(
        WeatherRepository weatherRepository,
        ILogger<WeatherService> logger)
    {
        _weatherRepository = weatherRepository;
        _logger = logger;
    }

    /// <summary>
    /// Асинхронный метод получения записей по месяцу и/или году.
    /// </summary>
    /// <param name="filters">Модель фильтров типа <see cref="WeatherFilters"/>.</param>
    /// <returns>Массив моделей <see cref="WeatherInfo"/>, которые подходят по указанным фильтрам.</returns>
    public async Task<IEnumerable<WeatherInfo>> GetWeatherByFiltersAsync(WeatherFilters filters)
    {
        try
        {
            return await _weatherRepository.GetWeatherByFiltersAsync(filters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception during in {nameof(GetWeatherByFiltersAsync)} at {nameof(WeatherService)}");
            throw;
        }
    }

    /// <summary>
    /// Метод вставки нескольких записей в таблицу.
    /// </summary>
    /// <param name="entities">Массив сущностей типа <see cref="IEnumerable{WeatherInfo}"/>.</param>
    /// <returns><see langword="true"/> если вставка прошла успешно, <see langword="false"/> если произошла ошибка.</returns>
    public async Task<bool> InsertBatchAsync(IEnumerable<WeatherInfo> entities)
    {
        try
        {
            return await _weatherRepository.InsertBatchAsync(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception during in {nameof(InsertBatchAsync)} at {nameof(WeatherService)}");
            throw;
        }
    }
}
