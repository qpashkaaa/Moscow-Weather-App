using Microsoft.AspNetCore.Mvc;

namespace MoscowWeatherApp.Server.Controllers;

/// <summary>
/// Контроллер стартовой страницы.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Метод отображения основной страницы.
    /// </summary>
    /// <returns>Представление страницы.</returns>
    public IActionResult Index()
    {
        return View();
    }
}
