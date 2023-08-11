using CoreAPIs.Entities;
using CoreAPIs.Models.DTO;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;

namespace CoreAPIs.Repository.Interface
{
    public interface IBasicLoanDetailsRep 
    {
        Task<GeneralModel> AddAsync(LoanDetailRequest request);
        Task<GeneralModel> UpdateAsync(UpdateLoanDetails request);
        Task<GeneralModel> DeleteAsync(LoanStatusUpdate request);
        Task<GeneralModel> Restore(LoanStatusUpdate request);
        Task<IEnumerable<LoanDetailsList>> GetAll();
        Task<LoanDetailRequest> DetailAsync(string id);
        Task<List<LoanActivityDTO>> GetActivity(LoanActivityFilter filter, bool Count);
    }
}
