using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portfoliobackend.Models;
using portfoliobackend.Services;

namespace portfoliobackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _service;
        public ProjectsController(ProjectService service)
        {
            _service = service;
        }

        //[HttpGet]
        //public IActionResult Gett() => Ok("working");

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAsync());

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var project = await _service.GetAsync(id);
            return project is null ? NotFound() : Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Project project)
        {
            await _service.CreateAsync(project);
            return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, Project project)
        {
            var existing = await _service.GetAsync(id);
            if (existing is null) return NotFound();

            project.Id = existing.Id;
            await _service.UpdateAsync(id, project);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _service.GetAsync(id);
            if (existing is null) return NotFound();

            await _service.RemoveAsync(id);
            return NoContent();
        }
    }
}

