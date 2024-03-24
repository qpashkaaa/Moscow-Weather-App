using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.Logging;
using MoscowWeatherApp.Core.Helpers;
using MoscowWeatherApp.Domain.Attributes;
using MoscowWeatherApp.Domain.Interfaces;
using MoscowWeatherApp.Domain.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MoscowWeatherApp.Core.Services;

/// <summary>
/// Cервис для работы с Weather info таблицами Excel.
/// </summary>
public class WeatherExcelSerivce : IExcelService<WeatherInfo, ExcelParameters>
{
    /// <summary>
    /// Логер.
    /// </summary>
    private readonly ILogger<WeatherExcelSerivce> _logger;

    /// <summary>
    /// Создание <see cref="WeatherExcelSerivce"/>.
    /// </summary>
    /// <param name="logger">Логер.</param>
    public WeatherExcelSerivce(ILogger<WeatherExcelSerivce> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Метод чтения листа таблицы.
    /// </summary>
    /// <param name="filePath">Путь до файла.</param>
    /// <param name="parameters">Модель параметров типа <see cref="ExcelParameters"/>.</param>
    /// <returns>Коллекцию <see cref="IEnumerable{WeatherInfo}"/> которая является телом таблицы.</returns>
    public IEnumerable<WeatherInfo> ReadTableSheet(string filePath, ExcelParameters parameters)
    {
        try
        {
            var weatherInfos = new List<WeatherInfo>();
            var formatter = new DataFormatter();
            var customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            Thread.CurrentThread.CurrentCulture = customCulture;

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                var workbook = new XSSFWorkbook(fileStream);
                var sheet = workbook.GetSheet(parameters.WorksheetName);
                var headerRow = sheet.GetRow(parameters.HeaderStartRow);
                List<string> headers = typeof(WeatherInfo)
                    .GetProperties()
                    .Where(prop => Attribute.IsDefined(prop, typeof(ExcelHeaderAttribute)))
                    .Select(prop => prop.GetCustomAttribute<ExcelHeaderAttribute>()!.Value)
                    .ToList();

                if (headers == null ||
                    headerRow == null || 
                    headerRow.Cells.Count != headers.Count)
                {
                    ExceptionsHelper.ThrowException(
                        "Строка заголовка не соответствует ожидаемым заголовкам.",
                        nameof(ReadTableSheet),
                        _logger);
                }

                foreach (var header in headers!)
                {
                    bool headerFound = false;
                    int headerIndex = headers.IndexOf(header);
                    foreach (var cell in headerRow!.Cells)
                    {
                        if (cell.StringCellValue.Contains(header, StringComparison.OrdinalIgnoreCase) &&
                            cell.ColumnIndex == headerIndex)
                        {
                            headerFound = true;
                            break;
                        }
                    }

                    if (!headerFound)
                    {
                        ExceptionsHelper.ThrowException(
                        $"Заголовок '{header}' не найден в строке заголовков или индекс этого столбца не совпадает с индексом свойства в модели '{nameof(WeatherInfo)}'.",
                        nameof(ReadTableSheet),
                        _logger);
                    }
                }

                for (int i = parameters.BodyStartRow; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);

                    if (row == null)
                    {
                        continue;
                    }

                    var weatherInfo = new WeatherInfo
                    {
                        Date = DateTime.TryParse(formatter.FormatCellValue(row.GetCell(0)), out DateTime date) ?
                        DateOnly.FromDateTime(date) : null,

                        Time = DateTime.TryParse(formatter.FormatCellValue(row.GetCell(1)), out DateTime time) ?
                        TimeOnly.FromDateTime(time) : null,

                        Temperature = double.TryParse(formatter.FormatCellValue(row.GetCell(2)), NumberStyles.Any, CultureInfo.CurrentCulture, out double temperature) ?
                        temperature : null,

                        RelativeHamidity = double.TryParse(formatter.FormatCellValue(row.GetCell(3)), NumberStyles.Any, CultureInfo.CurrentCulture, out double relativeHamidity) ?
                        relativeHamidity : null,

                        DewPoint = double.TryParse(formatter.FormatCellValue(row.GetCell(4)), NumberStyles.Any, CultureInfo.CurrentCulture, out double dewPoint) ?
                        dewPoint : null,

                        AtmosphericPressure = int.TryParse(formatter.FormatCellValue(row.GetCell(5)), NumberStyles.Any, CultureInfo.InvariantCulture, out int atmosphericPressure) ?
                        atmosphericPressure : null,

                        WindDirection = formatter.FormatCellValue(row.GetCell(6)),

                        WindSpeed = int.TryParse(formatter.FormatCellValue(row.GetCell(7)), NumberStyles.Any, CultureInfo.InvariantCulture, out int windSpeed) ?
                        windSpeed : null,

                        CloudCover = int.TryParse(formatter.FormatCellValue(row.GetCell(8)), NumberStyles.Any, CultureInfo.InvariantCulture, out int cloudCover) ?
                        cloudCover : null,

                        LowerLimitCloudCover = int.TryParse(formatter.FormatCellValue(row.GetCell(9)), NumberStyles.Any, CultureInfo.InvariantCulture, out int lowerLimitCloudCover) ?
                        lowerLimitCloudCover : null,

                        HorizontalVisibility = int.TryParse(formatter.FormatCellValue(row.GetCell(10)), NumberStyles.Any, CultureInfo.InvariantCulture, out int horizontalVisibility) ?
                        horizontalVisibility : null,

                        WeatherPhenomena = formatter.FormatCellValue(row.GetCell(11))
                    };

                    weatherInfos.Add(weatherInfo);
                }
            }

            return weatherInfos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception during in {nameof(ReadTableSheet)} at {nameof(WeatherExcelSerivce)}");
            throw;
        }
    }

    /// <summary>
    /// Метод получения имен всех листов таблицы.
    /// </summary>
    /// <param name="filePath">Путь до файла.</param>
    /// <returns>Коллекцию <see cref="IEnumerable{string}"/> с именами листов таблицы.</returns>
    public IEnumerable<string> GetAllSheetsNames(string filePath)
    {
        try
        {
            var sheetNames = new List<string>();

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                IWorkbook workbook = new XSSFWorkbook(fileStream);

                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    sheetNames.Add(sheet.SheetName);
                }
            }

            return sheetNames;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception during in {nameof(GetAllSheetsNames)} at {nameof(WeatherExcelSerivce)}");
            throw;
        }
    }
}
