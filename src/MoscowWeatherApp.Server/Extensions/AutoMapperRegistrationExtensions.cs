using MoscowWeatherApp.Server.Profiles;

namespace MoscowWeatherApp.Server.Extensions;

/// <summary>
/// Расширение для регистрации мапперов.
/// </summary>
public static class AutoMapperRegistrationExtensions
{
    /// <summary>
    /// Метод добавления мапперов для моделей Domain и DTO.
    /// </summary>
    public static IHostApplicationBuilder AddAutoMappers(
        this IHostApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(WeatherFiltersProfile));

        return builder;
    }
}
