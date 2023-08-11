using CoreAPIs.Entities;

namespace CoreAPIs.Models.Requests
{
    public class UserActivityFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string UserName { get; set; }
        public string IsActive { get; set; } = "Active";
        public string Email { get; set; }
    }
}
