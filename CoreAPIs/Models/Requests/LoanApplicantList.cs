namespace CoreAPIs.Models.Requests
{
    public class LoanApplicantList
    {
        public string LoanId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string MaritalStatus { get; set; }
        public string Email { get; set; }
        public string LoanType { get; set; }
        public string LoanAmount { get; set; }
        public bool Status { get; set;} 
    }
}
