using System.ComponentModel.DataAnnotations;

namespace Barely_Cooking_API.Models.DTO
{
    public class CakeUpdateDTO
    {
        [Key]
        public int CakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string SpecialTags { get; set; }
        [Range(1, int.MaxValue)]
        public double Price { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }

    }
}
