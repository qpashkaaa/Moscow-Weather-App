using MoscowWeatherApp.Domain.Interfaces;

namespace MoscowWeatherApp.Migration;

/// <summary>
/// Сервис для осуществелия миграций БД.
/// </summary>
public class MigrationHostedService : IHostedService
{
    /// <summary>
    /// Мигратор.
    /// </summary>
    private readonly IMigrator _migrator;

    /// <summary>
    /// Создание <see cref="MigrationHostedService"/>.
    /// </summary>
    /// <param name="migrator">Мигратор типа <see cref="IMigrator"/>.</param>
    public MigrationHostedService(IMigrator migrator)
    {
        _migrator = migrator;
    }

    /// <summary>
    /// Метод старта работы.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _migrator.MigrateAsync();
    }

    /// <summary>
    /// Метод остановки работы.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}