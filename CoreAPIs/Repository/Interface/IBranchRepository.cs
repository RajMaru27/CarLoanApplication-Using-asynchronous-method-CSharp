using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;

namespace CoreAPIs.Repository.Interface
{
    public interface IBranchRepository
    {
        Task<GeneralModel> AddAsync(BranchRequest request);
        Task<GeneralModel> UpdateAsync(BranchRequest request);
    }
}
