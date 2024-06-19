using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var issues = await _gitService.DownloadNewRepository(owner, repository);
            return Ok(issues);
        }

        [HttpPost("UpdateRepository/{repositoryId}")]
        public async Task<IActionResult> GetIssues(int repositoryId)
        {
            var issues = await _gitService.UpdateRepository(repositoryId);
            return Ok(issues);
        }
        
    }
}
