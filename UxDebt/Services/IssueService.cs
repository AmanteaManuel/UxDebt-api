using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UxDebt.Context;
using UxDebt.Entities;
using UxDebt.Models.Response;
using UxDebt.Models.Response.Dtos;
using UxDebt.Models.ViewModel;
using UxDebt.Services.Interfaces;

namespace UxDebt.Services
{
    public class IssueService : IIssueService
    {
        private readonly UxDebtContext _context;
        private readonly IMapper _mapper;

        public IssueService(UxDebtContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Create(IssueViewModel issue)
        {
            try
            {
                var issueEF = _mapper.Map<Issue>(issue);

                _context.Add(issueEF);
                await _context.SaveChangesAsync();
                return issueEF.IssueId; // Assuming IssueId is generated after save
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Create(List<Issue> issues)
        {
            try
            {
                _context.AddRange(issues);
                await _context.SaveChangesAsync();
                return true; 
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(int id, IssueViewModel issue)
        {
            var issueEF = await _context.Issues.FindAsync(id);
            if (issueEF == null) return false;

            // Actualiza las propiedades
            _mapper.Map(issue, issueEF);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var resultado = await _context.Issues.FirstOrDefaultAsync(x => x.IssueId == id);

            if (resultado == null)
            {
                return false;
            }

            try
            {
                _context.Issues.Remove(resultado);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetIssueViewModel> Get(int id)
        {
            try
            {
                return _mapper.Map<GetIssueViewModel>(await _context.Issues.FirstOrDefaultAsync(x => x.IssueId == id));                
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetIssueViewModel>> GetAllByRepoId(int id)
        {
            try
            {
                var issues = await _context.Issues
                                           .Where(x => x.RepositoryId == id).ToListAsync();

                return _mapper.Map<List<GetIssueViewModel>>(issues);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<GetIssueViewModel>> GetAll()
        {
            try
            {
                var issuesWithTags = await _context.Issues
                    .Include(issue => issue.IssueTags)
                        .ThenInclude(issueTag => issueTag.Tag)
                    .ToListAsync();

                var issueViewModels = _mapper.Map<List<GetIssueViewModel>>(issuesWithTags);                 
                
                return issueViewModels;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }
        

        public async Task<bool> SwitchDiscarded(int id)
        {
            try
            {
                var issue = await _context.Issues.FirstOrDefaultAsync(x => x.IssueId == id);
                if (issue == null)
                {
                    return false;
                }

                issue.Discarded = !issue.Discarded;

                _context.Entry(issue).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }       


        public async Task<PagedResult<GetIssueViewModel>> GetAllByFilter(FilterDto filter, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var issues = await GetAll(); // Assuming this fetches all issues efficiently

                // Combine filtering logic using LINQ Where clauses (more efficient)
                var filteredIssues = issues.Where(issue =>
                    (filter.Title == null ||  issue.Title.ToLower().Contains(filter.Title.ToLower())) &&
                    (filter.Discarded == null || issue.Discarded == filter.Discarded) &&
                    (filter.Status == null || issue.Status == filter.Status) &&
                    (filter.RepositoryId == null || issue.RepositoryId == filter.RepositoryId) &&
                    (filter.CreatedAt == null || issue.CreatedAt.Date == filter.CreatedAt?.Date));

                // Tag filtering using LINQ Any with pre-compiled predicate for efficiency
                if (filter.Tags != null && filter.Tags.Any())
                {
                    Func<GetIssueViewModel, bool> hasMatchingTag = issue => issue.Tags.Any(tag => filter.Tags.Contains(tag.TagId));
                    filteredIssues = filteredIssues.Where(hasMatchingTag);
                }

                // Pagination with Skip and Take
                int skip = (pageNumber - 1) * pageSize;
                var pagedIssues = filteredIssues.Skip(skip).Take(pageSize).ToList();

                // Return PagedResult with total count (optional)
                int totalCount = filteredIssues.Count();
                return new PagedResult<GetIssueViewModel>(pagedIssues, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

