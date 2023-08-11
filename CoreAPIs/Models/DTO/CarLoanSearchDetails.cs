using CoreAPIs.Entities;

namespace CoreAPIs.Models.DTO
{
    public class CarLoanSearchDetails 
    {  
        public string LoanId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string MaritalStatus { get; set; }
        public string Email { get; set; }    
        public string LoanType { get; set; }
        public string LoanAmount { get; set; }
        public bool ActivityStatus { get; set; }
    }
}
