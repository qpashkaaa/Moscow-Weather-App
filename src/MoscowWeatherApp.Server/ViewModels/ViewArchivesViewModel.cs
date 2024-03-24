using MoscowWeatherApp.Domain.Models;
using MoscowWeatherApp.Shared;

namespace MoscowWeatherApp.Server.ViewModels;

/// <summary>
/// ViewModel для view "ViewArchives".
/// </summary>
public class ViewArchivesViewModel
{
    /// <summary>
    /// Модель типа <see cref="IEnumerable{WeatherInfo}"/> с информацией о погоде.
    /// </summary>
    public required IEnumerable<WeatherInfo> WeatherInfo { get; set; }

    /// <summary>
    /// DTO модель фильтров.
    /// </summary>
    public required WeatherFiltersDTO WeatherFiltersDTO { get; set; }
}
