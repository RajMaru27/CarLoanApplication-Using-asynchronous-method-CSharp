using CoreAPIs.Entities;

namespace CoreAPIs.Models.DTO
{
    public class TaxPayerActivityDTO : CommonEntityFields
    {
        public string TaxPayerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxPayerStatus { get; set; }
    }
}
