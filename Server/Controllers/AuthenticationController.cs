using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IUserAccount accountInterface) : ControllerBase
    {
        [HttpPost("register", Name = "Authentication_Register")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync(Register user)
        {
            if (user == null) return BadRequest("Model is empty");

            var result = await accountInterface.CreateAsync(user);

            if (result is null) return StatusCode(StatusCodes.Status500InternalServerError);

            if (result.Success)
            {
                // Jeśli w result.Data jest nowo utworzony użytkownik z Id, zwróć 201 + Location
                // W innym razie można zwrócić 201 bez Location lub 200 OK
                var newUserId = TryGetId(result.Data);
                if (newUserId.HasValue)
                {
                    // Nie mamy dedykowanego GET-by-id w tym kontrolerze; zwrócimy 201 bez CreatedAtRoute
                    // Alternatywnie można dodać UsersController i użyć CreatedAtRoute do niego.
                    return StatusCode(StatusCodes.Status201Created, result);
                }
                return StatusCode(StatusCodes.Status201Created, result);
            }

            if (IsConflict(result.Message)) return Conflict(result);
            if (IsValidationError(result.Message)) return UnprocessableEntity(result);

            return BadRequest(result);
        }

        [HttpPost("login", Name = "Authentication_Login")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync(Login user)
        {
            if (user == null) return BadRequest("Model is empty");

            var result = await accountInterface.SignInAsync(user);

            if (result is null) return StatusCode(StatusCodes.Status500InternalServerError);

            // Udane logowanie -> 200 OK z tokenami itp.
            if (result.Success) return Ok(result);

            // Nieudane logowanie -> 401 Unauthorized
            return Unauthorized(result);
        }

        [HttpPost("refresh-token", Name = "Authentication_RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        {
            if (token == null) return BadRequest("Model is empty");

            var result = await accountInterface.RefreshTokenAsync(token);

            if (result is null) return StatusCode(StatusCodes.Status500InternalServerError);

            // Udane odświeżenie -> 200 OK
            if (result.Success) return Ok(result);

            // Nieudane odświeżenie -> 401 Unauthorized (token nieważny/nieprawidłowy)
            return Unauthorized(result);
        }

        [HttpGet("users", Name = "Authentication_GetUsers")]
        [Authorize]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await accountInterface.GetUsers();
            if (users == null) return NotFound();
            if (users.Count == 0) return NotFound();
            return Ok(users);
        }

        [HttpPut("update-user", Name = "Authentication_UpdateUser")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(ManageUser manageUser)
        {
            if (manageUser == null) return BadRequest("Model is empty");

            var result = await accountInterface.UpdateUser(manageUser);

            if (result is null) return StatusCode(StatusCodes.Status500InternalServerError);

            // Udana aktualizacja – 204 No Content (bez payloadu)
            if (result.Success) return NoContent();

            if (IsNotFound(result.Message)) return NotFound(result);
            if (IsConflict(result.Message)) return Conflict(result);
            if (IsValidationError(result.Message)) return UnprocessableEntity(result);

            return BadRequest(result);
        }

        //[HttpGet("roles", Name = "Authentication_GetRoles")]
        //[Authorize]
        //public async Task<IActionResult> GetRoles()
        //{
        //    var roles = await accountInterface.GetRoles();
        //    if (roles == null) return NotFound();
        //    if (roles.Count == 0) return NotFound();
        //    return Ok(roles);
        //}

        [HttpDelete("delete-user/{id}", Name = "Authentication_DeleteUser")]
        // 
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await accountInterface.DeleteUser(id);

            if (result is null) return StatusCode(StatusCodes.Status500InternalServerError);

            // Udane usunięcie – 204 No Content
            if (result.Success) return NoContent();

            if (IsNotFound(result.Message)) return NotFound(result);
            return BadRequest(result);
        }

        [HttpPut("update-profile", Name = "Authentication_UpdateProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UserProfile profile)
        {
            if (profile == null) return BadRequest("Model is empty");

            var success = await accountInterface.UpdateProfile(profile);

            // Udana aktualizacja – 204 No Content
            if (success) return NoContent();

            // Brak szczegółów – zwrócimy 400 (możesz rozwinąć UpdateProfile, by zwracać GeneralResponse)
            return BadRequest(new { Success = false, Message = "Unable to update profile" });
        }

        // Helpery do rozpoznawania typów błędów na podstawie komunikatu
        private static bool IsNotFound(string? msg)
            => msg?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true;

        private static bool IsConflict(string? msg)
            => msg?.Contains("exists", StringComparison.OrdinalIgnoreCase) == true
               || msg?.Contains("conflict", StringComparison.OrdinalIgnoreCase) == true
               || msg?.Contains("duplicate", StringComparison.OrdinalIgnoreCase) == true;

        private static bool IsValidationError(string? msg)
            => msg?.Contains("invalid", StringComparison.OrdinalIgnoreCase) == true
               || msg?.Contains("validation", StringComparison.OrdinalIgnoreCase) == true;

        private static int? TryGetId(object? data)
        {
            if (data is null) return null;
            var prop = data.GetType().GetProperty("Id");
            if (prop == null) return null;
            var value = prop.GetValue(data);
            return value is int i ? i : null;
        }
    }
}