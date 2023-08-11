using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;

namespace CoreAPIs.Repository.Interface
{
    public interface ITaxPayerFilingStatusRepository
    {
        Task<GeneralModel> AddAsync(TPFillingStatusRequest request);
        Task<List<TPFilingStatusList>> GetListAsync();    
    }
}
