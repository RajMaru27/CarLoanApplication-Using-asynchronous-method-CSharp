using CoreAPIs.Entities;

namespace CoreAPIs.Models.DTO
{
    public class UserActivityDTO : CommonEntityFields
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string Statuss { get; set; }
        public string Email { get; set; }
       
    }
}
