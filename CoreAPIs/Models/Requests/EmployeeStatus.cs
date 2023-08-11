using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Models.Requests
{
    public class EmployeeStatus
    {
        [Required]
        public string EmployeeId { get; set; }
    }
}
