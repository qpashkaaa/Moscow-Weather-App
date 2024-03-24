namespace MoscowWeatherApp.Domain.Interfaces;

/// <summary>
/// Интерфейс удаления данных из репозитория.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
public interface IDeleteRepository<TEntity>
    where TEntity : IEntity<long>
{
    /// <summary>
    /// Метод удаления записи по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns><see langword="true"/> если удаление прошло успешно, <see langword="false"/> если произошла ошибка.</returns>
    Task<bool> DeleteByIdAsync(long id);

    /// <summary>
    /// Метод удаления записей по их идентификаторам.
    /// </summary>
    /// <param name="ids">Массив идентификаторов типа <see cref="IEnumerable{long}"/></param>
    /// <returns><see langword="true"/> если удаление прошло успешно, <see langword="false"/> если произошла ошибка.</returns>
    Task<bool> DeleteByIdsAsync(IEnumerable<long> ids);
}