using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Code is lass than 3 characters")]
        [MaxLength(3, ErrorMessage = "Code is more than 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name is more than 100 characters")]
        public string Name { get; set; }
        public string? RegionImageURL { get; set; }
    }
}
