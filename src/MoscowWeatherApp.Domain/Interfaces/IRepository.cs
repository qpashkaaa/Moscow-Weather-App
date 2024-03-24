namespace MoscowWeatherApp.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
public interface IRepository<TEntity> :
    IReadRepository<TEntity>,
    IInsertRepository<TEntity>,
    IUpdateRepository<TEntity>,
    IDeleteRepository<TEntity>
    where TEntity : IEntity<long>
{
}