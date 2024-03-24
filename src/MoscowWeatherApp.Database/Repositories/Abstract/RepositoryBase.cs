using Microsoft.EntityFrameworkCore;
using MoscowWeatherApp.Domain.Interfaces;

namespace MoscowWeatherApp.Database.Repositories.Abstract;

/// <summary>
/// Абстрактный класс базового репозитория.
/// </summary>
public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity<long>
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    protected readonly DbContext _dbContext;

    /// <summary>
    /// Конструктор <see cref="RepositoryBase{TEntity}"/>.
    /// </summary>
    /// <param name="dbContext">Контекст БД.</param>
    protected RepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Метод получения всех записей из таблицы.
    /// </summary>
    /// <returns>Коллекцию типа <see cref="IEnumerable{TEntity}"/>.</returns>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// Получить запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Модель типа <see cref="TEntity"/>.</returns>
    public virtual async Task<TEntity?> GetByIdAsync(long id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// Получить записи по идентификаторам.
    /// </summary>
    /// <param name="ids">Массив идентификаторов типа <see cref="IEnumerable{long}"/>.</param>
    /// <returns>Коллекцию типа <see cref="IEnumerable{TEntity}"/>.</returns>
    public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<long> ids)
    {
        return await _dbContext.Set<TEntity>()
            .Where(e => ids.Contains(e.Id))
            .ToListAsync();
    }

    /// <summary>
    /// Получить записи с помощью пагинации.
    /// </summary>
    /// <param name="pageNumber">Номер строки.</param>
    /// <param name="pageSize">Количество записей.</param>
    /// <returns>Коллекцию типа <see cref="IEnumerable{TEntity}"/>.</returns>
    public virtual async Task<IEnumerable<TEntity>> GetPageAsync(int pageNumber, int pageSize)
    {
        return await _dbContext.Set<TEntity>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// Проверить существование элемента.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns><see langword="true"/> если элемент существует, <see langword="false"/> если элемент не существует.</returns>
    public virtual async Task<bool> IsExistsAsync(long id)
    {
        return await _dbContext.Set<TEntity>()
            .AnyAsync(e => e.Id == id);
    }

    /// <summary>
    /// Метод удаления записи по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns><see langword="true"/> если удаление прошло успешно, <see langword="false"/> если произошла ошибка.</returns>
    public virtual async Task<bool> DeleteByIdAsync(long id)
    {
        try
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            _dbContext.Entry(entity).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    /// <summary>
    /// Метод удаления записей по их идентификаторам.
    /// </summary>
    /// <param name="ids">Массив идентификаторов типа <see cref="IEnumerable{long}"/></param>
    /// <returns><see langword="true"/> если удаление прошло успешно, <see langword="false"/> если произошла ошибка.</returns>
    public virtual async Task<bool> DeleteByIdsAsync(IEnumerable<long> ids)
    {
        try
        {
            foreach (var id in ids)
            {
                await DeleteByIdAsync(id);
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    /// <summary>
    /// Метод вставки записи в таблицу.
    /// </summary>
    /// <param name="entity">Сущность типа <see cref="TEntity"/>.</param>
    /// <returns><see langword="true"/> если вставка прошла успешно, <see langword="false"/> если произошла ошибка.</returns>
    public virtual async Task<bool> InsertAsync(TEntity entity)
    {
        try
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    /// <summary>
    /// Метод вставки нескольких записей в таблицу.
    /// </summary>
    /// <param name="entities">Массив сущностей типа <see cref="IEnumerable{TEntity}"/>.</param>
    /// <returns><see langword="true"/> если вставка прошла успешно, <see langword="false"/> если произошла ошибка.</returns>
    public virtual async Task<bool> InsertBatchAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    /// <summary>
    /// Обновить запись в таблице.
    /// </summary>
    /// <param name="entity">Сущность типа <see cref="TEntity"/>.</param>
    /// <returns><see langword="true"/> если обновление прошло успешно, <see langword="false"/> если произошла ошибка.</returns>
    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    /// <summary>
    /// Метод обновления несколких записей в таблице.
    /// </summary>
    /// <param name="entities">Массив значений типа <see cref="IEnumerable{TEntity}"/>.</param>
    /// <returns><see langword="true"/> если обновление прошло успешно, <see langword="false"/> если произошла ошибка.</returns>
    public virtual async Task<bool> UpdateBatchAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }
}
