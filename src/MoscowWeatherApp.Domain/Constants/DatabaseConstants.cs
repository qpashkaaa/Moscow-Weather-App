namespace MoscowWeatherApp.Domain.Constants;

/// <summary>
/// Константы для БД.
/// </summary>
public static class DatabaseConstants
{
    /// <summary>
    /// Наименование схемы.
    /// </summary>
    public const string Schema = "WeatherApp";

    /// <summary>
    /// Название таблицы миграций.
    /// </summary>
    public const string MigrationsTableName = $"__EFMigrationsHistory";

    /// <summary>
    /// Название строки стандартного подключения, которая лежит в конфиге.
    /// </summary>
    public const string ConnectionStringName = "DefaultConnection";
}
