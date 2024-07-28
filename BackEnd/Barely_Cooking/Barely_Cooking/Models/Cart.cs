using System.ComponentModel.DataAnnotations.Schema;

namespace Barely_Cooking_API.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int CakeId { get; set; }

        [ForeignKey("CakeId")]
        public Cake Cake { get; set; } = new();

        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; }

    }
}
