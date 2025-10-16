
namespace BaseLibrary.Responses
{
    /// <summary>
    /// Standardowa odpowiedź logowania, spójna stylistycznie z GeneralResponse.
    /// </summary>
    public class LoginResponse
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
        /// Token dostępu (JWT).
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Token odświeżający.
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Opcjonalne dane (np. profil użytkownika, rola).
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// Konstruktor uproszczony bez danych i tokenów.
        /// </summary>
        public LoginResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// Konstruktor rozszerzony z tokenami.
        /// </summary>
        public LoginResponse(bool success, string message, string? token, string? refreshToken)
        {
            Success = success;
            Message = message;
            Token = token;
            RefreshToken = refreshToken;
        }

        /// <summary>
        /// Konstruktor rozszerzony z tokenami i danymi.
        /// </summary>
        public LoginResponse(bool success, string message, string? token, string? refreshToken, object? data)
        {
            Success = success;
            Message = message;
            Token = token;
            RefreshToken = refreshToken;
            Data = data;
        }

        public static LoginResponse NotFound(string what) =>
            new LoginResponse(false, $"{what} not found");

        public static LoginResponse Invalid(string message) =>
            new LoginResponse(false, message);

        public static LoginResponse Ok(string message, string? token = null, string? refreshToken = null, object? data = null) =>
            new LoginResponse(true, message, token, refreshToken, data);
    }
}
