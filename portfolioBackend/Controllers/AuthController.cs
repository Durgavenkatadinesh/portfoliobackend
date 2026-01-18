using Microsoft.AspNetCore.Mvc;
using portfolioBackend.Services;

namespace portfolioBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController:ControllerBase
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(AuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            var existing = await _authService.GetByEmailAsync(req.Email);
            if (existing != null)
                return BadRequest("User already exists");

            await _authService.RegisterAsync(req.Email, req.Password);
            return Ok("Registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var user = await _authService.GetByEmailAsync(req.Email);
            if(user==null)
                return Unauthorized("Invalid Credentials");

            if(!_authService.VerifyPassword(req.Password,user.PasswordHash))
                return Unauthorized("Invalid Credentials");

            var token = _tokenService.GenerateToken(user.Id!, user.Email);

            return Ok(new { token });
        }

    }
    public record RegisterRequest(string Email, string Password);
    public record LoginRequest(string Email, string Password);
}
