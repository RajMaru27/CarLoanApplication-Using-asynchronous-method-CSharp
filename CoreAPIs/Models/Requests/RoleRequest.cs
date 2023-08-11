using CoreAPIs.Entities;

namespace CoreAPIs.Models.Requests
{
    public class RoleRequest : CommonEntityFields
    {
        public string RoleName { get; set; }
        public string Salary { get; set; }
    }
}
