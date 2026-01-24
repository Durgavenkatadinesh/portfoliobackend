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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _authService.GetByEmailAsync(req.Email);
            if (existing != null)
                return BadRequest("User already exists");

            await _authService.RegisterAsync(req.Email, req.Password);
            return Ok("Registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.GetByEmailAsync(req.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            if (!_authService.VerifyPassword(req.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _tokenService.GenerateToken(user.Id!, user.Email);

            return Ok(new
            {
                token = token,
                userId = user.Id,
                email = user.Email
            });
        }
    }

    public record RegisterRequest(string Email, string Password);
    public record LoginRequest(string Email, string Password);
}
