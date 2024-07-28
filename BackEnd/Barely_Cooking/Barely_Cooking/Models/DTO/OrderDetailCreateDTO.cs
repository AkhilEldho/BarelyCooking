using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Barely_Cooking_API.Models.DTO
{
    public class OrderDetailCreateDTO
    {
        [Required]
        public int CakeId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string CakeName { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
