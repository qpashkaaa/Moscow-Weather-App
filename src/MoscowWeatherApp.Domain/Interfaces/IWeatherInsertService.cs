namespace MoscowWeatherApp.Domain.Interfaces;

/// <summary>
/// Интерфейс сервиса вставки записей о погоде.
/// </summary>
/// <typeparam name="TWeatherInfoModel">Модель информации о погоде.</typeparam>
public interface IWeatherInsertService<TWeatherInfoModel>
{
    /// <summary>
    /// Метод вставки нескольких записей в таблицу.
    /// </summary>
    /// <param name="entities">Массив сущностей типа <see cref="IEnumerable{TWeatherInfoModel}"/>.</param>
    /// <returns><see langword="true"/> если вставка прошла успешно, <see langword="false"/> если произошла ошибка.</returns>
    Task<bool> InsertBatchAsync(IEnumerable<TWeatherInfoModel> entities);
}
