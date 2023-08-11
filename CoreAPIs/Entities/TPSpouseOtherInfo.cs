using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Entities
{
    public class TPSpouseOtherInfo : CommonEntityFields
    {
        public string SpouseId { get; set; }
        [Required]
        public string Occupation { get; set; }
        [Required]
        public string FullTimeStudent { get; set; }
        [Required]
        public string PermanentlyDisabled { get; set; }
        [Required]
        public string LegallyBlind { get; set; }
        [Required]
        public string IsIndependent { get; set; }
    }
}
