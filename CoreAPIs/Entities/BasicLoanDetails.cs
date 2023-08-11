using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities
{
    public class BasicLoanDetails : CommonEntityFields
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
