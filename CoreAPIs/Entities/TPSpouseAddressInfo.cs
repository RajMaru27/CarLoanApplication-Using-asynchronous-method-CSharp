using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities
{
    public class TPSpouseAddressInfo : CommonEntityFields
    {
        public string SpouseId { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}
