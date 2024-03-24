namespace MoscowWeatherApp.Domain.Attributes;

/// <summary>
/// Атрибут заголовка столбца Excel.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ExcelHeaderAttribute : Attribute
{
    /// <summary>
    /// Значение.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Создание <see cref="ExcelHeaderAttribute"/>.
    /// </summary>
    /// <param name="value">Значение.</param>
    public ExcelHeaderAttribute(string value)
    {
        Value = value;
    }
}
