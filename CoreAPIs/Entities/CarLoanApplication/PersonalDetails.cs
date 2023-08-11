using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreAPIs.Entities.CarLoanApplication
{
    public class PersonalDetails : CommonEntityFields
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string EmergencyContactNumber { get; set; }
        public string DriversLicenseNumber { get; set; }

    }
}
