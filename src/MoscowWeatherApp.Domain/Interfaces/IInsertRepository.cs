namespace MoscowWeatherApp.Domain.Interfaces;

/// <summary>
/// Интерфейс добавления данных в репозиторий.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
public interface IInsertRepository<TEntity>
    where TEntity : IEntity<long>
{
    /// <summary>
    /// Метод вставки записи в таблицу.
    /// </summary>
    /// <param name="entity">Сущность типа <see cref="TEntity"/>.</param>
    /// <returns><see langword="true"/> если вставка прошла успешно, <see langword="false"/> если произошла ошибка.</returns>
    Task<bool> InsertAsync(TEntity entity);

    /// <summary>
    /// Метод вставки нескольких записей в таблицу.
    /// </summary>
    /// <param name="entities">Массив сущностей типа <see cref="IEnumerable{TEntity}"/>.</param>
    /// <returns><see langword="true"/> если вставка прошла успешно, <see langword="false"/> если произошла ошибка.</returns>
    Task<bool> InsertBatchAsync(IEnumerable<TEntity> entities);
}