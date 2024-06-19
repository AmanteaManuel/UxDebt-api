using Microsoft.AspNetCore.Mvc;
using UxDebt.Entities;
using UxDebt.Models.ViewModel;

namespace UxDebt.Services.Interfaces
{
    public interface ITagService
    {
        Task<int> AddTagToIssue(string codeTag, int issueId);
        Task<int> Create(TagViewModel tag);
        Task<bool> Delete(int id);
        Task<Tag> Get(int id);
        Task<List<Tag>> GetAll();
    }
}
