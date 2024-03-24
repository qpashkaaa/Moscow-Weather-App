using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoscowWeatherApp.Database.Data;
using MoscowWeatherApp.Domain.Interfaces;

namespace MoscowWeatherApp.Database;

/// <summary>
/// Сервис мигратора.
/// </summary>
public class Migrator : IMigrator
{
    /// <summary>
    /// Провайдер сервисов.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Создание <see cref="Migrator"/>.
    /// </summary>
    /// <param name="serviceProvider"></param>
    public Migrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Метод выполнения миграции.
    /// </summary>
    public async Task MigrateAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MoscowWeatherAppDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
