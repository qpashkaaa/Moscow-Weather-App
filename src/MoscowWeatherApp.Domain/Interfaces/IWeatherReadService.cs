namespace MoscowWeatherApp.Domain.Interfaces;

/// <summary>
/// Интерфейс сервиса чтения погоды.
/// </summary>
/// <typeparam name="TWeatherInfoModel">Модель информации о погоде.</typeparam>
/// <typeparam name="TFiltersModel">Модель фильтров.</typeparam>
public interface IWeatherReadService<TWeatherInfoModel, TFiltersModel>
{
    /// <summary>
    /// Метод получения списка погоды по фильтрам.
    /// </summary>
    /// <param name="filters">Модель фильров.</param>
    /// <returns>Коллекцию <see cref="IEnumerable{TWeatherModel}"/> представляющую список погоды по указанным фильтрам.</returns>
    Task<IEnumerable<TWeatherInfoModel>> GetWeatherByFiltersAsync(TFiltersModel filters);
}