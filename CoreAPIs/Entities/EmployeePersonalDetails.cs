using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities
{
    public class EmployeePersonalDetails : CommonEntityFields
    {
        [Required]
        public string Name { get; set; }
        public string Age { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
