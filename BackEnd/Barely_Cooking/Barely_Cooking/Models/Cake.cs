using System.ComponentModel.DataAnnotations;

namespace Barely_Cooking_API.Models
{
    public class Cake
    {
        [Key]
        public int CakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string SpecialTags { get; set; }
        public string Category { get; set; }
        [Range(1, int.MaxValue)]
        public double Price { get; set; }
        [Required]
        public string ImageFile { get; set; }

    }
}
