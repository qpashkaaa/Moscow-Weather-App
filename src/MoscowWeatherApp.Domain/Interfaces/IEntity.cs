namespace MoscowWeatherApp.Domain.Interfaces;

/// <summary>
/// Интерфейс базовой сущности.
/// </summary>
/// <typeparam name="TId">Тип Id.</typeparam>
public interface IEntity<TId> where TId : struct
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    TId Id { get; set; }
}
