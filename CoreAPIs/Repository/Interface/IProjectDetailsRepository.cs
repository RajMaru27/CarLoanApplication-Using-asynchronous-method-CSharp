using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;

namespace CoreAPIs.Repository.Interface
{
    public interface IProjectDetailsRepository
    {
        Task<GeneralModel> AddAsync(ProjectDetailRequest request);
        Task<GeneralModel> UpdateAsync(ProjectDetailRequest request);
        Task<GeneralModel> DeleteAsync(ProDetailStatusUpdate request);
        Task<GeneralModel> Restore(ProDetailStatusUpdate request);
        Task<IEnumerable<ProDetailStatus>> List();
    }
}
