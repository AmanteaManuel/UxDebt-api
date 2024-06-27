using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UxDebt.Entities;
using UxDebt.Models.Response.Dtos;
using UxDebt.Response;
using UxDebt.Services.Interfaces;

namespace UxDebt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitController : ControllerBase
    {
        private readonly IGitService _gitService;

        public GitController(IGitService gitService)
        {
            _gitService = gitService;
        }

        [HttpPost("DownloadNewRepository/{owner}/{repository}")]
        public async Task<IActionResult> DownloadNewRepository(string owner, string repository)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var issues = await _gitService.DownloadNewRepository(owner, repository);
                if(!issues.IsSuccess)
                    return StatusCode((int)issues.ResponseCode , $"Internal server error: {issues.Message}");
                return Ok(issues);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }

        [HttpPost("UpdateRepository/{repositoryId}")]
        public async Task<IActionResult> GetIssues(int repositoryId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var issues = await _gitService.UpdateRepository(repositoryId);
                if (!issues.IsSuccess)
                    return StatusCode((int)issues.ResponseCode, $"Internal server error: {issues.Message}");
                return Ok(issues);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

           
        }
        
    }
}
