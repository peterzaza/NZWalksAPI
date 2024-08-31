using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class UpdateWalkRequestDtocs
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name is more than 100 Characters.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name is more than 1000 Characters.")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageURL { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
