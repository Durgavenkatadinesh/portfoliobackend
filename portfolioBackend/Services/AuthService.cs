using MongoDB.Driver;
using portfolioBackend.Models;
using BCrypt.Net;

namespace portfolioBackend.Services
{
    public class AuthService
    {
        private readonly IMongoCollection<User> _users;

        public AuthService(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("Users");
        }

        // ---------- HELPERS ----------
        public async Task<User?> GetByIdentifierAsync(string identifier)
        {
            return await _users.Find(
                u => u.Email == identifier || u.Username == identifier
            ).FirstOrDefaultAsync();
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _users.Find(u => u.Username == username).AnyAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _users.Find(u => u.Email == email).AnyAsync();
        }

        // ---------- REGISTER ----------
        public async Task<User> RegisterAsync(
            string username,
            string email,
            string password,
            string? phoneNumber
        )
        {
            if (await UsernameExistsAsync(username))
                throw new Exception("Username already exists");

            if (await EmailExistsAsync(email))
                throw new Exception("Email already exists");

            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = username.ToLower(),
                Email = email.ToLower(),
                PasswordHash = hash,
                PhoneNumber = phoneNumber,
                IsSubscribed = false,
                CreatedAt = DateTime.UtcNow
            };

            await _users.InsertOneAsync(user);
            return user;
        }

        // ---------- LOGIN ----------
        public async Task<User?> LoginAsync(string identifier, string password)
        {
            var user = await GetByIdentifierAsync(identifier);
            if (user == null)
                return null;

            var valid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!valid)
                return null;

            return user;
        }
    }
}
