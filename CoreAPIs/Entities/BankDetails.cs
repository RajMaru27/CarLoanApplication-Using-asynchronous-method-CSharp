using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities
{
    public class BankDetails : CommonEntityFields
    {
        public string LoanId { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string AccNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
