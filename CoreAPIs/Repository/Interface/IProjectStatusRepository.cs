using CoreAPIs.Entities;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;

namespace CoreAPIs.Repository.Interface
{
    public interface IProjectStatusRepository
    {
        Task<GeneralModel> AddAsync(StatusRequest request);
        Task<IEnumerable<StatusRequest>> GetListAsync();
    }

    
}
