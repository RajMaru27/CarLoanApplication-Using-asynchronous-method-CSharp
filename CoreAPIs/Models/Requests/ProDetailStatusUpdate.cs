using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Models.Requests
{
    public class ProDetailStatusUpdate
    {
        [Required]
        public string Id { get; set; }
    }
}
