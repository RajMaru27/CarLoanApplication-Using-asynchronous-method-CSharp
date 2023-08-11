namespace CoreAPIs.Models.Requests
{
    public class UpdateLoanDetails
    {
        public UpdateBasicDetails basicDetails { get; set; }
        public UpdateAddressDetails addressDetails { get; set; }
        public UpdateRequiredLoanDetails loanDetails { get; set; }
        public UpdateEmploymentDetails employmentDetails { get; set; }
        public UpdateBankDetails bankDetails { get; set; }

        public class UpdateBasicDetails
        {
            public string Id { get; set; }  
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string MaritalStatus { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }

        public class UpdateAddressDetails
        {
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public string LivingSince { get; set; }
        }

        public class UpdateRequiredLoanDetails
        {
            public int DesiredLoanAmt { get; set; }
            public int AnnualIncome { get; set; }
            public string LoanType { get; set; }
            public int Downpayment { get; set; }
        }

        public class UpdateEmploymentDetails
        {
            public string EmployerName { get; set; }
            public string Occupation { get; set; }
            public string WorkExperience { get; set; }
            public int GrossMonthlyIncome { get; set; }
            public int MonthlyExpense { get; set; }
        }

        public class UpdateBankDetails
        {
            public string BankName { get; set; }
            public string AccNo { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
