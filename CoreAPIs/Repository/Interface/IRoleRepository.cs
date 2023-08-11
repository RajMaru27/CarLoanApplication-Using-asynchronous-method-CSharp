using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;

namespace CoreAPIs.Repository.Interface
{
    public interface IRoleRepository
    {
        Task<GeneralModel> AddAsync(RoleRequest request);
        Task<GeneralModel> UpdateAsync(RoleRequest request);
    }
}
