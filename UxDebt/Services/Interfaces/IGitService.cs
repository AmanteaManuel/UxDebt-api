using UxDebt.Dtos;
using UxDebt.Entities;
using UxDebt.Response;

namespace UxDebt.Services.Interfaces
{
    public interface IGitService
    {
        Task<MultipleResponse<Issue>> DownloadNewRepository(string owner, string repository);
        Task<SingleResponse<bool>> UpdateRepository(int repoId);        
    }
}
