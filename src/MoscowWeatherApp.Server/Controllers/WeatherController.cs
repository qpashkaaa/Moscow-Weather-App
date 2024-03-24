using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoscowWeatherApp.Domain.Constants;
using MoscowWeatherApp.Domain.Interfaces;
using MoscowWeatherApp.Domain.Models;
using MoscowWeatherApp.Server.ViewModels;
using MoscowWeatherApp.Shared;

namespace MoscowWeatherApp.Server.Controllers;

/// <summary>
/// Контроллер для взаимодействия с погодными архивами.
/// </summary>
public class WeatherController : Controller
{
    /// <summary>
    /// Сервис для работы с ифнормацией о погоде.
    /// </summary>
    private readonly IWeatherService<WeatherInfo, WeatherFilters> _weatherService;

    /// <summary>
    /// Cервис для работы с Weather info таблицами Excel.
    /// </summary>
    private readonly IExcelService<WeatherInfo, ExcelParameters> _weatherExcelService;

    /// <summary>
    /// Логер.
    /// </summary>
    private readonly ILogger<WeatherController> _logger;

    /// <summary>
    /// Маппер.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Создание <see cref="WeatherController"/>.
    /// </summary>
    /// <param name="weatherService">Сервис для работы с ифнормацией о погоде.</param>
    /// <param name="weatherExcelService">Cервис для работы с Weather info таблицами Excel.</param>
    public WeatherController(
        IWeatherService<WeatherInfo, WeatherFilters> weatherService,
        IExcelService<WeatherInfo, ExcelParameters> weatherExcelService,
        ILogger<WeatherController> logger,
        IMapper mapper)
    {
        _weatherService = weatherService;
        _weatherExcelService = weatherExcelService;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Метод отображения страницы загрузки архивов.
    /// GET: Weather/UploadArchives.
    /// </summary>
    /// <returns>Представление страницы.</returns>
    public IActionResult UploadArchives()
    {
        return View();
    }

    /// <summary>
    /// Метод отображения страницы просмотра сохраненных архивов.
    /// GET: Weather/ViewArchives.
    /// </summary>
    /// <returns>Представление страницы.</returns>
    public async Task<IActionResult> ViewArchives()
    {
        var weatherFiltersDTO = new WeatherFiltersDTO
        {
            PageNumber = 1
        };

        var viewModel = new ViewArchivesViewModel 
        {
            WeatherFiltersDTO = weatherFiltersDTO,
            WeatherInfo = await GetWeatherByFilters(weatherFiltersDTO)
        };

        return View("ViewArchives", viewModel);
    }

    /// <summary>
    /// POST Метод загрузки погодных архивов Excel.
    /// </summary>
    /// <param name="files">Список файлов.</param>
    /// <returns>View со статусом обработки файлов.</returns>
    [HttpPost]
    public async Task<IActionResult> UploadExcelArchives(List<IFormFile> files)
    {
        var filesCount = files.Count;
        var errorUploadFilesCount = 0;

        if (files == null || files.Count == 0)
        {
            ViewBag.Message = "Файлы не выбраны.";
            return View("UploadArchives");
        }

        foreach (var file in files)
        {
            if (file.ContentType == "application/vnd.ms-excel" || file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                try
                {
                    var filePath = SaveFile(file);

                    var sheetNames = _weatherExcelService.GetAllSheetsNames(filePath);

                    foreach (var sheetName in sheetNames)
                    {
                        var parameters = new ExcelParameters
                        {
                            WorksheetName = sheetName,
                            HeaderStartRow = 2,
                            BodyStartRow = 4
                        };
                        var weatherData = _weatherExcelService.ReadTableSheet(filePath, parameters);

                        await _weatherService.InsertBatchAsync(weatherData);
                    }
                }
                catch (Exception ex)
                {
                    errorUploadFilesCount++;
                    _logger.LogError(ex, $"FileName: {file.FileName}. Exception during in {nameof(UploadExcelArchives)} at {nameof(WeatherController)}");
                }
            }
        }

        ViewBag.Message = $"Успешно загружено {filesCount - errorUploadFilesCount} из {filesCount} файлов.";
        return View("UploadArchives");
    }

    /// <summary>
    /// Метод получения записей по месяцу и/или году.
    /// </summary>
    /// <param name="weatherFiltersDTO">DTO Модель фильтров типа <see cref="WeatherFiltersDTO"/>.</param>
    /// <returns>View со статусом обработки запроса.</returns>
    [HttpPost]
    public async Task<IActionResult> GetWeatherTableByFilters(WeatherFiltersDTO weatherFiltersDTO)
    {
        var viewModel = new ViewArchivesViewModel
        {
            WeatherFiltersDTO = weatherFiltersDTO,
            WeatherInfo = await GetWeatherByFilters(weatherFiltersDTO)
        };

        return View("ViewArchives", viewModel);
    }

    /// <summary>
    /// Сохранить файл, полученный с формы (внутренний метод).
    /// </summary>
    /// <param name="file">Файл, отправленный с HTTP-запросом.</param>
    /// <returns>Путь до сохраненного файла.</returns>
    private string SaveFile(IFormFile file)
    {
        var filePath = Path.Combine(ExcelConstants.DefaultExcelsOutputDir, file.FileName);

        if (!Directory.Exists(ExcelConstants.DefaultExcelsOutputDir))
        {
            Directory.CreateDirectory(ExcelConstants.DefaultExcelsOutputDir);
        }

        using (var stream = System.IO.File.Create(filePath))
        {
            file.CopyTo(stream);
        }

        return filePath;
    }

    /// <summary>
    /// Метод получения записей по месяцу и/или году (внутренний метод).
    /// </summary>
    /// <param name="weatherFiltersDTO">DTO Модель фильтров типа <see cref="WeatherFiltersDTO"/>.</param>
    /// <returns>Массив записей по указанным фильтрам.</returns>
    private async Task<IEnumerable<WeatherInfo>> GetWeatherByFilters(WeatherFiltersDTO weatherFiltersDTO)
    {
        var weatherFilers = _mapper.Map<WeatherFilters>(weatherFiltersDTO);

        return await _weatherService.GetWeatherByFiltersAsync(weatherFilers);
    }
}
