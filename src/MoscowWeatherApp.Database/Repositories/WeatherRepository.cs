using Microsoft.EntityFrameworkCore;
using MoscowWeatherApp.Database.Data;
using MoscowWeatherApp.Database.Repositories.Abstract;
using MoscowWeatherApp.Domain.Models;

namespace MoscowWeatherApp.Database.Repositories;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="WeatherInfo"/>.
/// </summary>
public class WeatherRepository : RepositoryBase<WeatherInfo>
{
    /// <summary>
    /// Создание <see cref="WeatherRepository"/>.
    /// </summary>
    /// <param name="dbContext"></param>
    public WeatherRepository(MoscowWeatherAppDbContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// Асинхронный метод получения записей по месяцу и/или году.
    /// </summary>
    /// <param name="filters">Модель фильтров типа <see cref="WeatherFilters"/>.</param>
    /// <returns>Массив моделей <see cref="WeatherInfo"/>, которые подходят по указанным фильтрам.</returns>
    public async Task<IEnumerable<WeatherInfo>> GetWeatherByFiltersAsync(WeatherFilters filters)
    {
        var query = _dbContext.Set<WeatherInfo>().AsQueryable();

        if (filters.Month.HasValue)
        {
            query = query.Where(x => x.Date.HasValue && x.Date.Value.Month == filters.Month.Value.Month);
        }

        if (filters.Year.HasValue)
        {
            query = query.Where(x => x.Date.HasValue && x.Date.Value.Year == filters.Year.Value.Year);
        }

        return await query
            .OrderBy(x => x.Date)
            .Skip((filters.PageNumber - 1) * filters.PageSize)
            .Take(filters.PageSize)
            .ToListAsync();
    }
}