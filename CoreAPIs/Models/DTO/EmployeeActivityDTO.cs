using CoreAPIs.Entities;

namespace CoreAPIs.Models.DTO
{
    public class EmployeeActivityDTO : CommonEntityFields
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string EmpStatus { get; set; }
    }
}
