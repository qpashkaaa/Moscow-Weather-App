using Microsoft.Extensions.Logging;

namespace MoscowWeatherApp.Core.Helpers;

/// <summary>
/// Помощник по исключениям.
/// </summary>
public static class ExceptionsHelper
{
    /// <summary>
    /// Пробросить ошибку по полю null.
    /// </summary>
    /// <param name="nameofNullElement">Имя элемента, который равен null.</param>
    /// <param name="nameofExceptionMethod">Имя метода, в котором произошла ошибка.</param>
    /// <param name="logger">Логгер.</param>
    public static void ThrowArgumentNullException(
        string nameofNullElement,
        string nameofExceptionMethod,
        ILogger? logger = null)
    {
        var ex = new ArgumentNullException(nameofNullElement);

        if (logger != null)
        {
            logger.LogError(ex, $"Exception during in {nameofExceptionMethod} method.");
        }

        throw ex;
    }

    /// <summary>
    /// Пробросить кастомную ошибку.
    /// </summary>
    /// <param name="message">Сообщение ошибки.</param>
    /// <param name="nameofExceptionMethod">Имя метода, в котором произошла ошибка.</param>
    /// <param name="logger">Логгер.</param>
    public static void ThrowException(
        string message,
        string nameofExceptionMethod,
        ILogger? logger = null)
    {
        var ex = new Exception(message);

        if (logger != null)
        {
            logger.LogError(ex, $"Exception during in {nameofExceptionMethod} method.");
        }

        throw ex;
    }
}