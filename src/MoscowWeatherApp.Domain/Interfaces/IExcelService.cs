namespace MoscowWeatherApp.Domain.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с таблицами Excel.
/// </summary>
/// <typeparam name="TResponseModel">Модель ответа.</typeparam>
/// <typeparam name="TParametersModel">Модель параметров.</typeparam>
public interface IExcelService<TResponseModel, TParametersModel> : 
    IExcelReadService<TResponseModel, TParametersModel>
{
}
