using MoscowWeatherApp.Domain.Attributes;
using MoscowWeatherApp.Domain.Interfaces;

namespace MoscowWeatherApp.Domain.Models;

/// <summary>
/// Модель информации о погоде.
/// Порядок свойств повторяет порядок данных в таблице Excel.
/// </summary>
public class WeatherInfo : IEntity<long>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Дата.
    /// </summary>
    [ExcelHeader("Дата")]
    public DateOnly? Date { get; set; }

    /// <summary>
    /// Время.
    /// </summary>
    [ExcelHeader("Время")]
    public TimeOnly? Time { get; set; }

    /// <summary>
    /// Температура.
    /// </summary>
    [ExcelHeader("Т")]
    public double? Temperature { get; set; }

    /// <summary>
    /// Относительная влажность воздуха.
    /// </summary>
    [ExcelHeader("Отн. влажность")]
    public double? RelativeHamidity { get; set; }

    /// <summary>
    /// Температура точки росы.
    /// </summary>
    [ExcelHeader("Td")]
    public double? DewPoint { get; set; }

    /// <summary>
    /// Атмосферное давление.
    /// </summary>
    [ExcelHeader("Атм. давление")]
    public int? AtmosphericPressure { get; set; }

    /// <summary>
    /// Направление.
    /// </summary>
    [ExcelHeader("Направление")]
    public string? WindDirection { get; set; }

    /// <summary>
    /// Скорость ветра.
    /// </summary>
    [ExcelHeader("Скорость")]
    public int? WindSpeed { get; set; }

    /// <summary>
    /// Облачность.
    /// </summary>
    [ExcelHeader("Облачность")]
    public int? CloudCover { get; set; }

    /// <summary>
    /// Нижняя граница облачности.
    /// </summary>
    [ExcelHeader("h")]
    public int? LowerLimitCloudCover { get; set; }

    /// <summary>
    /// Горизонтальная видимость.
    /// </summary>
    [ExcelHeader("VV")]
    public int? HorizontalVisibility { get; set; }

    /// <summary>
    /// Погодные явления.
    /// </summary>
    [ExcelHeader("Погодные явления")]
    public string? WeatherPhenomena { get; set; }
}
