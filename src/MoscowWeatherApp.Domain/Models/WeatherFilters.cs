namespace MoscowWeatherApp.Domain.Models;

/// <summary>
/// Модель фильтров для сервиса погоды.
/// </summary>
public class WeatherFilters
{
    /// <summary>
    /// Номер страницы.
    /// </summary>
    private int _pageNumber;

    /// <summary>
    /// Размер страницы.
    /// </summary>
    private int _pageSize;

    /// <summary>
    /// Фильр по месяцу.
    /// </summary>
    public DateOnly? Month { get; set; }

    /// <summary>
    /// Фильтр по году.
    /// </summary>
    public DateOnly? Year { get; set; }

    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int PageNumber
    {
        get
        {
            return _pageNumber;
        }
        set
        {
            _pageNumber = Math.Max(value, 1);
        }
    }

    /// <summary>
    /// Размер страницы.
    /// </summary>
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = Math.Max(value, 15);
        }
    }
}
