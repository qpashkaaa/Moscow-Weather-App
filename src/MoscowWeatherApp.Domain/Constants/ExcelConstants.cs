namespace MoscowWeatherApp.Domain.Constants;

/// <summary>
/// Константы для сервиса Excel.
/// </summary>
public class ExcelConstants
{
    /// <summary>
    /// Стандартный путь сохранения файлов Excel.
    /// </summary>
    public static string DefaultExcelsOutputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"archives\\{DateTime.Now.Date.ToString("dd-MM-yyyy")}");
}
