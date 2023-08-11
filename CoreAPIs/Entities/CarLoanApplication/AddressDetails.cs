using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities.CarLoanApplication
{
    public class AddressDetails : CommonEntityFields
    {
        public string LoanId { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set;}
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}
