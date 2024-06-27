using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System;
using UxDebt.Dtos;
using UxDebt.Response;
using UxDebt.Services.Interfaces;
using UxDebt.Entities;
using System.Net;
using UxDebt.Models.ViewModel;
using AutoMapper;
using UxDebt.Models.Response.Dtos;
using System.Transactions;

namespace UxDebt.Services
{
    public class GitService : IGitService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IIssueService _issueService;
        private readonly IRepositoryService _repositoryService;
        private readonly IMapper _mapper;

        public string Owner { get; set; }
        public string Repository { get; set; }

        public GitService(IConfiguration configuration, HttpClient httpClient, IIssueService issueService, IRepositoryService repositoryService, IMapper mapper)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _issueService = issueService;
            _repositoryService = repositoryService;
            _mapper = mapper;
        }
        
        public async Task<MultipleResponse<Issue>> DownloadNewRepository(string repositoryOwner, string repositoryName)
        {
            MultipleResponse<Issue> response = new MultipleResponse<Issue>();
            int repoId;

            try
            {
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    
                    #region "Repo Validation and Creation"

                    //Get the issue from the DB
                    Repository repo = await _repositoryService.Get(repositoryOwner, repositoryName);
                    if (repo == null)
                        //get the repos's from git and insert to the DB
                        repoId = await CreateRepoFromGit(repositoryOwner, repositoryName);
                    else
                        repoId = repo.RepositoryId;

                    #endregion                    

                    var issuesDtos = new List<IssueDto>();
                    issuesDtos = await GetAllIssuesFromGit(repositoryOwner, repositoryName);

                    var issuesToAdded = await AddIssuesToDB(repoId, issuesDtos);

                    transaction.Complete();

                    return response.SetResponse(true, HttpStatusCode.OK, "Ok", null);
                }
            }
            catch (Exception ex)
            {

                return response.SetResponse(false, HttpStatusCode.InternalServerError, $"Error downloading repository: {ex.Message}", null);
                
            }
            

        }              

        private async Task<int> CreateRepoFromGit(string repositoryOwner, string repositoryName)
        {
            var repoFromGit = await GetRepository(repositoryOwner, repositoryName);


            RepositoryViewModel repository = new RepositoryViewModel
            {
                GitId = repoFromGit.Data.GitId,
                Owner = repoFromGit.Data.Owner.Login,
                Name = repoFromGit.Data.Name,
                Description = repoFromGit.Data.Description,
                HtmlUrl= repoFromGit.Data.HtmlUrl,
            };

            return await _repositoryService.Create(repository);
        }

        public async Task<SingleResponse<bool>> UpdateRepository(int repoId)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();

            try
            {
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {


                    // 1. Get repository details
                    var repo = await _repositoryService.Get(repoId);
                    if (repo == null)
                    {
                        return response.SetResponse(false, HttpStatusCode.BadRequest, "The repository is not added on the BD", false);
                    }

                    // 2. Fetch existing issues in the repository
                    var existingIssues = await _issueService.GetAllByRepoId(repoId);

                    // 3. Retrieve all issues from external source (e.g., Git)
                    var allIssues = await GetAllIssuesFromGit(repo.Owner, repo.Name);

                    // 4. Identify new issues (not present in existingIssues)
                    var newIssues = allIssues.Where(gitIssue => !existingIssues.Any(existing => existing.GitId == gitIssue.GitId)).ToList();

                    // 5. Add new issues to the repository (adapt based on your repository management system)
                    if (newIssues.Any())
                    {                       
                        await AddIssuesToDB(repoId, newIssues);                        
                        response.SetResponse(true, HttpStatusCode.OK, "New issues added successfully.", true);
                    }
                    else
                    {
                        response.SetResponse(true, HttpStatusCode.OK, "No new issues found.", true);
                    }

                    // 6. Update existing issues with new data
                    foreach (GetIssueViewModel existingIssue in existingIssues)
                    {
                        //get the issue from the new list of issues
                        var updatedIssue = allIssues.FirstOrDefault(gitIssue => gitIssue.GitId == existingIssue.GitId);
                        bool needUpdate = false;
                        //if already exist(should never be false)
                        if (updatedIssue != null)
                        {
                            //conver labels to string and update with the new if i must
                            var labelsTostring = LabelsToString(updatedIssue);
                            if (labelsTostring != existingIssue.Labels)
                            {
                                existingIssue.Labels = labelsTostring;
                                needUpdate = true;
                            }                       
                            
                            //if the close date change change it
                            if (updatedIssue.ClosedAt != existingIssue.ClosedAt)
                            {
                                existingIssue.ClosedAt = updatedIssue.ClosedAt;
                                needUpdate = true;
                            }

                            if(needUpdate)
                            {
                                //mapp the new object and update
                                var i = _mapper.Map<IssueViewModel>(existingIssue);
                                await _issueService.Update(existingIssue.IssueId, i);
                            }
                            needUpdate = false;
                        }
                    }

                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                return response.SetResponse(false, HttpStatusCode.InternalServerError, $"Error updating repository: {ex.Message}", false);
            }
            return response;
        }

        private async Task<List<Issue>> AddIssuesToDB(int repoId, List<IssueDto> issuesFromGit)
        {
            List<Issue> issuesToAdd = new List<Issue>();
            foreach (IssueDto issue in issuesFromGit)
            {
                string concatenatedLabels = LabelsToString(issue);

                Issue i = new Issue()
                {
                    GitId = issue.GitId,
                    HtmlUrl = issue.HtmlUrl,
                    Discarded = false,
                    CreatedAt = issue.CreateAt,
                    ClosedAt = issue.ClosedAt,
                    Status = issue.Status,
                    Title = issue.Title,
                    Labels = concatenatedLabels,
                    RepositoryId = repoId
                };

                issuesToAdd.Add(i);
            }

            await _issueService.Create(issuesToAdd);
            return issuesToAdd;
        }

        private static string LabelsToString(IssueDto issue)
        {
            return issue.Labels != null
                            ? string.Join(", ", issue.Labels.Select(label => label.Name))
                            : string.Empty;
        }

        #region "API GIT"

        public async Task<MultipleResponse<IssueDto>> GetIssues(string owner, string repository)
        {
            MultipleResponse<IssueDto> response = new MultipleResponse<IssueDto>();
            HttpClient client = new HttpClient();


            client.DefaultRequestHeaders.Add("User-Agent", "request");

            string url = $"{_configuration.GetSection("External").GetSection("Git").Value}/{owner.ToLower()}/{repository.ToLower()}/issues";


            var dataResponse = await client.GetAsync(url);

            if (!dataResponse.IsSuccessStatusCode)                
                return response.SetResponse(dataResponse.IsSuccessStatusCode, dataResponse.StatusCode, $"It was an error consumin GIT API", null);            

            List<IssueDto> issueResponses = JsonConvert.DeserializeObject<List<IssueDto>>(await dataResponse.Content.ReadAsStringAsync());


            return response.SetResponse(dataResponse.IsSuccessStatusCode, dataResponse.StatusCode, "Ok", issueResponses);

        }

        public async Task<SingleResponse<RepositoryDto>> GetRepository(string owner, string repository)
        {
            SingleResponse<RepositoryDto> response = new SingleResponse<RepositoryDto>();
            HttpClient client = new HttpClient();


            client.DefaultRequestHeaders.Add("User-Agent", "request");

            string url = $"{_configuration.GetSection("External").GetSection("Git").Value}/{owner.ToLower()}/{repository.ToLower()}";

            var dataResponse = await client.GetAsync(url);

            if (!dataResponse.IsSuccessStatusCode)
                return response.SetResponse(dataResponse.IsSuccessStatusCode, dataResponse.StatusCode, $"It was an error consumin GIT API", null);

            RepositoryDto issueResponses = JsonConvert.DeserializeObject<RepositoryDto>(await dataResponse.Content.ReadAsStringAsync());


            return response.SetResponse(dataResponse.IsSuccessStatusCode, dataResponse.StatusCode, "Ok", issueResponses);

        }

        private async Task<List<IssueDto>> GetAllIssuesFromGit(string repositoryOwner, string repositoryName)
        {
            var data = new List<IssueDto>();
            var nextPattern = new Regex(@"(?<=<)([\S]*)(?=>; rel=""next"")", RegexOptions.IgnoreCase);
            bool pagesRemaining = true;
            string url = $"{_configuration.GetSection("External").GetSection("Git").Value}/{repositoryOwner.ToLower()}/{repositoryName.ToLower()}/issues";

            while (pagesRemaining)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url); 
                request.Headers.Add("Authorization", "Bearer " + _configuration.GetSection("External").GetSection("GitKey").Value);
                request.Headers.Add("State", "all");

                _httpClient.DefaultRequestHeaders.Add("User-Agent", "request");

                var responseSendAsync = await _httpClient.SendAsync(request);
                responseSendAsync.EnsureSuccessStatusCode();

                var responseData = await responseSendAsync.Content.ReadAsStringAsync();
                var parsedData = JsonConvert.DeserializeObject<List<IssueDto>>(responseData);

                data.AddRange(parsedData);

                if (responseSendAsync.Headers.TryGetValues("Link", out var linkHeaderValues))
                {
                    var linkHeader = linkHeaderValues.FirstOrDefault();
                    pagesRemaining = linkHeader != null && linkHeader.Contains("rel=\"next\"");

                    if (pagesRemaining)
                    {
                        url = nextPattern.Match(linkHeader).Groups[1].Value;
                    }
                }
                else
                {
                    pagesRemaining = false;
                }
            }

            return data;
        }

        #endregion

    }
}
