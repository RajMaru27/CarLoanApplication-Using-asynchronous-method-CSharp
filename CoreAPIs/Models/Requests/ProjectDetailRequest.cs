using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Models.Requests
{
    public class ProjectDetailRequest
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string StatusId { get; set; }
        [Required]
        public string ClientCompany { get; set; }
        public string ProjectLeader { get; set; }
    }
}
