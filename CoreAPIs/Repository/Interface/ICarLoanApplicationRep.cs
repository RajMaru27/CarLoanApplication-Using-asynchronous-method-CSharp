using CoreAPIs.Models.DTO;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;

namespace CoreAPIs.Repository.Interface
{
    public interface ICarLoanApplicationRep
    {
        Task<GeneralModel> AddAsync(CarLoanAppRequest request);
        Task<GeneralModel> UpdateAsync(CarLoanAppUpdate request);
        Task<IEnumerable<LoanApplicantList>> GetAll();
        Task<GeneralModel> Delete(LoanApplicantStatus request);
        Task<GeneralModel> Restore(LoanApplicantStatus request);
        Task<List<CarLoanSearchDetails>> FilterActivity(CarLoanSearchList filter);
        Task<CarLoanApplicantDetails> Details(string LoanId);
        Task<List<LoanApplicantList>> InActiveListOnly();
    }
}
