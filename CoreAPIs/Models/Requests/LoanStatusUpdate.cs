using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Models.Requests
{
    public class LoanStatusUpdate
    {
        [Required]
        public string LoanId { get; set; }
    }
}
