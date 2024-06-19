using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UxDebt.Entities;
using UxDebt.Models.ViewModel;
using UxDebt.Services.Interfaces;

namespace UxDebt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        private readonly IRepositoryService _repositoryService;

        public RepositoryController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] RepositoryViewModel repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _repositoryService.Create(repository);
                return CreatedAtAction(nameof(Get), new { id = id }, repository);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RepositoryViewModel repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _repositoryService.Update(id, repository))
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!await _repositoryService.Delete(id))
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Repository>> Get(int id)
        {
            try
            {
                var repository = await _repositoryService.Get(id);
                if (repository == null)
                {
                    return NotFound();
                }
                return repository;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Repository>>> GetAll()
        {
            try
            {
                return await _repositoryService.GetAll();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
