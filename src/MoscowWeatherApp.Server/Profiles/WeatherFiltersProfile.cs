using AutoMapper;
using MoscowWeatherApp.Domain.Models;
using MoscowWeatherApp.Shared;

namespace MoscowWeatherApp.Server.Profiles;

/// <summary>
/// Профиль для маппинга <see cref="WeatherFiltersDTO"/> и <see cref="WeatherFilters"/>.
/// </summary>
public class WeatherFiltersProfile : Profile
{
    /// <summary>
    /// Создание <see cref="WeatherFiltersDTO"/>.
    /// </summary>
    public WeatherFiltersProfile()
    {
        CreateMap<WeatherFiltersDTO, WeatherFilters>();
    }
}
