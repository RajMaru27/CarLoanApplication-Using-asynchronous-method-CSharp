using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Models.Requests
{
    public class UserStatusUpdate
    {
        [Required]
        public string UserId { get; set; }
    }
}
