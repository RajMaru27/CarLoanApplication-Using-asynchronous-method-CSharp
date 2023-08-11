using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities.CarLoanApplication
{
    public class FinancialDetails : CommonEntityFields
    {
        public string LoanId { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string Branch { get; set;}
        [Required]
        public string AccountType { get; set;}
        [Required]
        public string AccountNumber { get; set;}
        [Required]
        public string OwnOrRent { get; set;}
        [Required]
        public string CurrentLoan { get; set;}
        [Required]
        public string WorkingStatus { get; set;}
        [Required]
        public string MonthlyIncome { get; set;}
    }
}
