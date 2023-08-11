using CoreAPIs.Models.DTO;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;

namespace CoreAPIs.Repository.Interface
{
    public interface IEmployeeDetails
    {
        Task<GeneralModel> AddAsync(EmployeelDetailRequest request);
        Task<GeneralModel> UpdateAsync(UpdateEmployeeDetails request);
        Task<IEnumerable<EmployeeDetailList>> GetAll();
        Task<GeneralModel> DeleteAsync(EmployeeStatus empstatus);
        Task<GeneralModel> Restore(EmployeeStatus empstatus);
        Task<IEnumerable<EmployeeActivityDTO>> ActivityList(EmployeeActivityFilter filter, bool Count);
    }
}
