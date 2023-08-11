using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities
{
    public class EmployeeWorkDetail : CommonEntityFields
    {
        public string EmployeeId { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string WorkingSince { get; set; }
        [Required]
        public string AnnualSalary { get; set; }
    }
}
