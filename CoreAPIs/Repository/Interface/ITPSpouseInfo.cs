using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;

namespace CoreAPIs.Repository.Interface
{
    public interface ITPSpouseInfo
    {
        Task<GeneralModel> AddAsync(TPSpouseInfoRequest request);
        Task<GeneralModel> UpdateAsync(TPSpouseInfoUpdate request);
    }
}
