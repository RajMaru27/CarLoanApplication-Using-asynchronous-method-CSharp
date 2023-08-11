using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Models.Requests
{
    public class TaxPayerActivityStatus
    {
        [Required]
        public string TaxPayerId { get; set; }
    }
}
