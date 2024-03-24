namespace MoscowWeatherApp.Domain.Interfaces;

/// <summary>
/// Интерфейс мигратора.
/// </summary>
public interface IMigrator
{
    /// <summary>
    /// Метод выполнения миграции.
    /// </summary>
    Task MigrateAsync();
}
