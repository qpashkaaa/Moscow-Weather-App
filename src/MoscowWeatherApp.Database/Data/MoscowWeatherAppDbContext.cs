using Microsoft.EntityFrameworkCore;
using MoscowWeatherApp.Domain.Constants;
using MoscowWeatherApp.Domain.Models;

namespace MoscowWeatherApp.Database.Data;

/// <summary>
/// Контекст БД.
/// </summary>
public class MoscowWeatherAppDbContext : DbContext
{
    /// <summary>
    /// Сущности информации о погоде.
    /// </summary>
    public DbSet<WeatherInfo> WeatherInfo { get; set; }

    /// <summary>
    /// Создание <see cref="MoscowWeatherAppDbContext"/>.
    /// </summary>
    /// <param name="options">Настройки контекста БД.</param>
    public MoscowWeatherAppDbContext(DbContextOptions<MoscowWeatherAppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Метод, вызываемый при создании модели.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DatabaseConstants.Schema);
    }
}
