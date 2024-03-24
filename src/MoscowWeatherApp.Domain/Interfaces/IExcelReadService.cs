namespace MoscowWeatherApp.Domain.Interfaces;

/// <summary>
/// Интерфейс сервиса чтения таблицы Excel.
/// </summary>
/// <typeparam name="TResponseModel">Модель ответа.</typeparam>
/// <typeparam name="TParametersModel">Модель параметров.</typeparam>
public interface IExcelReadService<TResponseModel, TParametersModel>
{
    /// <summary>
    /// Метод чтения листа таблицы.
    /// </summary>
    /// <param name="filePath">Путь до файла.</param>
    /// <param name="parameters">Модель параметров типа <see cref="TParametersModel"/>.</param>
    /// <returns>Коллекцию <see cref="IEnumerable{TResponseModel}"/> которая является телом таблицы.</returns>
    IEnumerable<TResponseModel> ReadTableSheet(
        string filePath,
        TParametersModel parameters);

    /// <summary>
    /// Метод получения имен всех листов таблицы.
    /// </summary>
    /// <param name="filePath">Путь до файла.</param>
    /// <returns>Коллекцию <see cref="IEnumerable{string}"/> с именами листов таблицы.</returns>
    IEnumerable<string> GetAllSheetsNames(string filePath);
}