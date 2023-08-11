using CoreAPIs.Entities;
using CoreAPIs.Models.DTO;
using CoreAPIs.Models.Requests;

namespace CoreAPIs.Models.Response
{
    public class BaseApiResponse
    {
        public BaseApiResponse()
        {

        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
        public List<Users> users { get; set; }
        public List<UserList> userList { get; set; }
        public object Data { get; set; }
        public List<StatusRequest> projectStatuses { get; set; }
        public List<ProDetailStatus> ProDetails { get; set; }
        public List<LoanDetailsList> LoanDetailsLists { get; set; }
        public List <EmployeeDetailList> employeeDetailLists { get; set; }
        public List<TPFilingStatusList> tPFilingStatusLists { get; set;}
        public List<TaxPayerInfoList> taxPayerInfoList { get; set;}
        public List<LoanApplicantList> loanApplicantLists { get; set; }
    }
    public class BaseApiPostResponse<T> : BaseApiResponse
    {
        public virtual T Data1 { get; set; }
    }
}
