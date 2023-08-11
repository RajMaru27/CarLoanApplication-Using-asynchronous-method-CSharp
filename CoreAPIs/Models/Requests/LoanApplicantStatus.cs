using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Models.Requests
{
    public class LoanApplicantStatus
    {
        [Required]
        public string LoanId { get; set;}
    }
}
