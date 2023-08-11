using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities
{
    public class TPSpounsePersonalInfo : CommonEntityFields
    {
        [Required]
        public string TaxPayerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
