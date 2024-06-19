using UxDebt.Entities;
using UxDebt.Models.ViewModel;

namespace UxDebt.Services.Interfaces
{
    public interface IRepositoryService
    {
        Task<int> Create(RepositoryViewModel repository);
        Task<bool> Update(int id, RepositoryViewModel repository);
        Task<bool> Delete(int id);
        Task<Repository> Get(int id);
        Task<List<Repository>> GetAll();
        Task<Repository> Get(string owner, string repository);
        Task<Repository> GetWithIssues(int id);
    }
}
