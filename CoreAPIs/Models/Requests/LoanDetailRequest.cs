namespace CoreAPIs.Models.Requests
{
    public class LoanDetailRequest
    {
        public BasicDetails basicDetails { get; set; }
        public AddressDetails addressDetails { get; set; }
        public RequiredLoanDetails loanDetails { get; set; }
        public EmploymentDetails employmentDetails { get; set; }
        public BankDetails bankDetails { get; set; }

        public class BasicDetails
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string MaritalStatus { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }

        public class AddressDetails
        {
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public string LivingSince { get; set; }
        }

        public class RequiredLoanDetails
        {
            public int DesiredLoanAmt { get; set; }
            public int AnnualIncome { get; set; }
            public string LoanType { get; set; }
            public int Downpayment { get; set; }
        }

        public class EmploymentDetails
        {
            public string EmployerName { get; set; }
            public string Occupation { get; set; }
            public string WorkExperience { get; set; }
            public int GrossMonthlyIncome { get; set; }
            public int MonthlyExpense { get; set; }
        }

        public class BankDetails
        {
            public string BankName { get; set;}
            public string AccNo { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
