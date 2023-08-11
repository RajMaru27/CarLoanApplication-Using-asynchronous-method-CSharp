using CoreAPIs.Entities;

namespace CoreAPIs.Models.Requests
{
    public class BranchRequest : CommonEntityFields
    {
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string country { get; set; }
    }
}
