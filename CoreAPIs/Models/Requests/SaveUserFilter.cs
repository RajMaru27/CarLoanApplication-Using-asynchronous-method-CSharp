using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Models.Requests
{
    public class SaveUserFilter
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FilterName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
