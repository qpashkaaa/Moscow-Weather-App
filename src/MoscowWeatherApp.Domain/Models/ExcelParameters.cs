namespace MoscowWeatherApp.Domain.Models;

/// <summary>
/// Модель параметров Excel.
/// </summary>
public class ExcelParameters
{
    /// <summary>
    /// Строка, с которой начинается "шапка таблицы" (внутренняя переменная).
    /// </summary>
    private int _headerStartRow;

    /// <summary>
    /// Строка, с которой начинается "тело документа" (внутренняя переменная).
    /// </summary>
    private int _bodyStartRow;

    /// <summary>
    /// Имя рабочего листа.
    /// </summary>
    public string? WorksheetName { get; set; }

    /// <summary>
    /// Строка, с которой начинается "шапка документа".
    /// </summary>
    public int HeaderStartRow
    {
        get
        {
            return _headerStartRow;
        }
        set
        {
            _headerStartRow = Math.Max(value, 1);
        }
    }

    /// <summary>
    /// Строка, с которой начинается "тело документа".
    /// </summary>
    public int BodyStartRow
    {
        get
        {
            return _bodyStartRow;
        }
        set
        {
            _bodyStartRow = Math.Max(value, 2);
        }
    }
}
