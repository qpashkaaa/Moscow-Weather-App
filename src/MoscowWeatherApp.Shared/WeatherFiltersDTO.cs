namespace MoscowWeatherApp.Shared;

/// <summary>
/// DTO Модель фильтров для сервиса погоды.
/// </summary>
public record WeatherFiltersDTO
{
    /// <summary>
    /// Номер страницы (внутренняя переменная).
    /// </summary>
    private int _pageNumber;

    /// <summary>
    /// Размер страницы (внутренняя переменная).
    /// </summary>
    private int _pageSize;

    /// <summary>
    /// Фильр по месяцу (внутренняя переменная).
    /// </summary>
    private DateOnly? _month;

    /// <summary>
    /// Фильтр по году (внутренняя переменная).
    /// </summary>
    private DateOnly? _year;

    /// <summary>
    /// Число месяца для формирования <see cref="Month"/>.
    /// </summary>
    public int MonthNumber { get; init; }

    /// <summary>
    /// Число года для формирования <see cref="Year"/>.
    /// </summary>
    public int YearNumber { get; init; }

    /// <summary>
    /// Фильр по месяцу.
    /// </summary>
    public DateOnly? Month 
    { 
        get 
        {
            if (_month == null && MonthNumber > 0)
            {
                _month = new DateOnly(1, MonthNumber, 1);
            }

            return _month;
        } 
        init 
        {
            _month = value;
        } 
    }

    /// <summary>
    /// Фильтр по году.
    /// </summary>
    public DateOnly? Year
    {
        get
        {
            if (_year == null && YearNumber > 0)
            {
                _year = new DateOnly(YearNumber, 1, 1);
            }

            return _year;
        }
        init
        {
            _year = value;
        }
    }

    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int PageNumber
    {
        get
        {
            return _pageNumber;
        }
        init
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
        init
        {
            _pageSize = Math.Max(value, 15);
        }
    }
}