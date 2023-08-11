using CoreAPIs.Models.DTO;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;

namespace CoreAPIs.Repository.Interface
{
    public interface ITaxPayerInfo
    {
        Task<GeneralModel> AddAsync(TaxPayerInfoRequest request);
        Task<GeneralModel> UpdateAsync(TaxPayerUpdate request);
        Task<IEnumerable<TaxPayerInfoList>> GetList();
        Task<GeneralModel> Delete(TaxPayerActivityStatus activityStatus);
        Task<GeneralModel> Restore(TaxPayerActivityStatus activityStatus);
        Task<List<TaxPayerActivityDTO>> ActivityList(TaxPayerActivityFilter filter, bool count);
    }
}
