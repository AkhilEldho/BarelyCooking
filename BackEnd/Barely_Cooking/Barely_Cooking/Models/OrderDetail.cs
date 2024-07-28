using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Barely_Cooking_API.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailsId { get; set; }
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int CakeId { get; set; }

        [ForeignKey("CakeId")]
        public Cake Cake { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public string CakeName { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
