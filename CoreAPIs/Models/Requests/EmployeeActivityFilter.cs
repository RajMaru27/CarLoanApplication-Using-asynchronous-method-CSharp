namespace CoreAPIs.Models.Requests
{
    public class EmployeeActivityFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IsActive { get; set; } = "Active";

    }
}
