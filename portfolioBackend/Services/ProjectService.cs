using MongoDB.Driver;
using portfoliobackend.Models;

namespace portfoliobackend.Services
{
    public class ProjectService
    {
        private readonly IMongoCollection<Project> _projects;
        public ProjectService(IMongoDatabase database)
        {
            _projects = database.GetCollection<Project>("Projects");
        }

        public async Task<List<Project>> GetAsync() =>
            await _projects.Find(_ => true).ToListAsync();

        public async Task<Project?> GetAsync(string id) =>
            await _projects.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Project project) =>
            await _projects.InsertOneAsync(project);

        public async Task UpdateAsync(string id, Project updated) =>
            await _projects.ReplaceOneAsync(x => x.Id == id, updated);

        public async Task RemoveAsync(string id) =>
            await _projects.DeleteOneAsync(x => x.Id == id);
    }
}
