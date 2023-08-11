using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities
{
    public class LoanAddressDetails : CommonEntityFields
    {
        public string LoanId { get; set; }
        [Required]
        public string Line1 { get; set; }
        [Required]
        public string Line2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string LivingSince { get; set; }
    }
}
