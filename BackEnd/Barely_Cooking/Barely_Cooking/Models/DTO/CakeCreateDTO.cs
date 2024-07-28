using System.ComponentModel.DataAnnotations;

namespace Barely_Cooking_API.Models.DTO
{
    public class CakeCreateDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string SpecialTags { get; set; }
        public string Category { get; set; }
        [Range(1, int.MaxValue)]
        public double Price { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
