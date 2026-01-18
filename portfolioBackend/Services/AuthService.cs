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
        public async Task<User?> GetByEmailAsync(string email) =>
            await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task RegisterAsync(string email, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User
            {
                Email = email,
                PasswordHash = hash
            };
            await _users.InsertOneAsync(user);

        }
        public bool VerifyPassword(string password, string hash) =>
            BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
