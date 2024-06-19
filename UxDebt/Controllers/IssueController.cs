using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UxDebt.Entities;
using UxDebt.Models.Response;
using UxDebt.Models.Response.Dtos;
using UxDebt.Models.ViewModel;
using UxDebt.Services;
using UxDebt.Services.Interfaces;

namespace UxDebt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] IssueViewModel issue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _issueService.Create(issue);
                return CreatedAtAction(nameof(Get), new { id = id }, issue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IssueViewModel issue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _issueService.Update(id, issue))
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

        [HttpPost("SwitchDiscarded/{id}")]
        public async Task<IActionResult> SwitchDiscarded(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _issueService.SwitchDiscarded(id))
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
                if (!await _issueService.Delete(id))
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
        public async Task<ActionResult<GetIssueViewModel>> Get(int id)
        {
            try
            {
                var issue = await _issueService.Get(id);
                if (issue == null)
                {
                    return NotFound();
                }
                return issue;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetIssueViewModel>>> GetAll()
        {
            try
            {
                return await _issueService.GetAll();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("GetAllByFilter/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PagedResult<GetIssueViewModel>>> GetAllByFilter([FromBody] FilterDto filter,int pageNumber, int pageSize)
        {
            try
            {
                return await _issueService.GetAllByFilter(filter,pageNumber,pageSize);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}