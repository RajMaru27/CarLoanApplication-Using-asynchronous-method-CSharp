namespace CoreAPIs.Models.Requests
{
    public class LoanDetailsList
    {
        public string LoanId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DesiredLoanAMT { get; set; }
        public string LoanType { get; set; }
    }
}
