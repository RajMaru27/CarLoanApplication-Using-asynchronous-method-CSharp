using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities
{
    public class EmployeeEducationDetails : CommonEntityFields
    {
        public string EmployeeId { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        public string QualificationYear { get; set; }
        [Required]
        public string University { get; set; }
    }
}
