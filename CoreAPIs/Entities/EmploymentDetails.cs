using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities
{
    public class EmploymentDetails : CommonEntityFields
    {
        public string LoanId { get; set; }
        [Required]
        public string EmployerName { get; set; }
        [Required]
        public string Occupation { get; set; }
        [Required]
        public string WorkExperience { get; set; }
        [Required]
        public int GrossMonthlyIncome { get; set; }
        [Required]
        public int MonthlyExpense { get; set; }
    }
}
