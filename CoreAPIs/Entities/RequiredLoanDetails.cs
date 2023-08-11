using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities
{
    public class RequiredLoanDetails : CommonEntityFields
    {
        public string LoanId { get; set; }
        [Required]
        public int DesiredLoanAmt { get; set; }
        [Required]
        public int AnnualIncome { get; set; }
        [Required]
        public string LoanType { get; set; }
        [Required]
        public int Downpayment { get; set; }
    }
}
