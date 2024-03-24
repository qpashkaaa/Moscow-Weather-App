namespace MoscowWeatherApp.Domain.Interfaces;

/// <summary>
/// Интерфейс обновления данных в репозитории.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
public interface IUpdateRepository<TEntity>
    where TEntity : IEntity<long>
{
    /// <summary>
    /// Обновить запись в таблице.
    /// </summary>
    /// <param name="entity">Сущность типа <see cref="TEntity"/>.</param>
    /// <returns><see langword="true"/> если обновление прошло успешно, <see langword="false"/> если произошла ошибка.</returns>
    Task<bool> UpdateAsync(TEntity entity);

    /// <summary>
    /// Метод обновления несколких записей в таблице.
    /// </summary>
    /// <param name="entities">Массив значений типа <see cref="IEnumerable{TEntity}"/>.</param>
    /// <returns><see langword="true"/> если обновление прошло успешно, <see langword="false"/> если произошла ошибка.</returns>
    Task<bool> UpdateBatchAsync(IEnumerable<TEntity> entities);
}