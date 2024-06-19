using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UxDebt.Entities;
using UxDebt.Models.ViewModel;
using UxDebt.Services.Interfaces;

namespace UxDebt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] TagViewModel tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _tagService.Create(tag);
                return CreatedAtAction(nameof(Get), new { id = id }, tag);
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
                if (!await _tagService.Delete(id))
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
        public async Task<ActionResult<Tag>> Get(int id)
        {
            try
            {
                var tag = await _tagService.Get(id);
                if (tag == null)
                {
                    return NotFound();
                }
                return tag;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Tag>>> GetAll()
        {
            try
            {
                return await _tagService.GetAll();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddTagToIssue")]
        public async Task<ActionResult<int>> AddTagToIssue([FromBody] string codeTag, int issueId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _tagService.AddTagToIssue(codeTag,issueId);
                return CreatedAtAction(nameof(Get), new { id = id }, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
