
namespace BaseLibrary.Responses
{
    /// <summary>
    /// Standardowa odpowiedź z repozytoriów / serwisów
    /// </summary>
    public class GeneralResponse
    {
        /// <summary>
        /// Czy operacja zakończyła się sukcesem.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Wiadomość dla użytkownika lub logów.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Opcjonalne dane (lista, obiekt, itp.)
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// Konstruktor uproszczony bez danych.
        /// </summary>
        public GeneralResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// Konstruktor rozszerzony z danymi.
        /// </summary>
        public GeneralResponse(bool success, string message, object? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static GeneralResponse NotFound(string what) =>
            new GeneralResponse(false, $"{what} not found");

        public static GeneralResponse Invalid(string message) =>
            new GeneralResponse(false, message);

        public static GeneralResponse Ok(string message, object? data = null) =>
            new GeneralResponse(true, message, data);

    }
}

