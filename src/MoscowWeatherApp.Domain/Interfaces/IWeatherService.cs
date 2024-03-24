namespace MoscowWeatherApp.Domain.Interfaces;

/// <summary>
/// Погодный сервис.
/// </summary>
/// <typeparam name="TWeatherInfoModel">Модель информации о погоде.</typeparam>
/// <typeparam name="TFiltersModel">Модель фильтров.</typeparam>
public interface IWeatherService<TWeatherInfoModel, TFiltersModel> :
    IWeatherReadService<TWeatherInfoModel, TFiltersModel>,
    IWeatherInsertService<TWeatherInfoModel>
{
}