using System.ComponentModel.DataAnnotations;

namespace NZwalksDpAPI.Models.DTO
{
    public class UpdateRegionDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be 3 characters")]
        [MaxLength(3)]
        public string Code { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
