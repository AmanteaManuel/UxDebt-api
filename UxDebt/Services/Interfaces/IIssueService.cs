using Microsoft.AspNetCore.Mvc;
using UxDebt.Context;
using UxDebt.Entities;
using UxDebt.Models.Response;
using UxDebt.Models.Response.Dtos;
using UxDebt.Models.ViewModel;

namespace UxDebt.Services.Interfaces
{
    public interface IIssueService
    {
        Task<int> Create(IssueViewModel issue);
        Task<List<int>> Create(List<Issue> issue);

        Task<bool> Update(int id, IssueViewModel issue);
        Task<bool> Delete(int id);
        Task<GetIssueViewModel> Get(int id);
        Task<List<GetIssueViewModel>> GetAllByRepoId(int id);

        Task<List<GetIssueViewModel>> GetAll();
        Task<bool> SwitchDiscarded(int id);
        Task<PagedResult<GetIssueViewModel>> GetAllByFilter(FilterDto filter, int pageNumber = 1, int pageSize = 10);

    }
}
