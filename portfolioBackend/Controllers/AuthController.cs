using Microsoft.AspNetCore.Mvc;
using portfolioBackend.Services;

namespace portfolioBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(AuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        // ---------- REGISTER ----------
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("All fields are required");
            }

            try
            {
                var user = await _authService.RegisterAsync(
                    request.Username,
                    request.Email,
                    request.Password,
                    request.PhoneNumber
                );

                var token = _tokenService.GenerateToken(user.Id, user.Email);

                return Ok(new
                {
                    message = "Registration succes",
                    userId = user.Id,
                    username = user.Username,
                    token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ---------- LOGIN ----------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.LoginAsync(
                request.Identifier,
                request.Password
            );

            if (user == null)
                return Unauthorized("Invalid username/email or password");
            var token = _tokenService.GenerateToken(user.Id, user.Email);

            return Ok(new
            {
                message = "Login successful",
                userId = user.Id,
                username = user.Username,
                isSubscribed = user.IsSubscribed,
                token

            });
        }
    }

    // ---------- DTOs ----------
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
    }

    public class LoginRequest
    {
        public string Identifier { get; set; } = string.Empty; // username OR email
        public string Password { get; set; } = string.Empty;
    }
}
