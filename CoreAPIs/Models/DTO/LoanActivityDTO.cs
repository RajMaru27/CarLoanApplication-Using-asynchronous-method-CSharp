using CoreAPIs.Entities;

namespace CoreAPIs.Models.DTO
{
    public class LoanActivityDTO : CommonEntityFields
    {
        public string LoanId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string LoanStatus { get; set; }
    }
}
