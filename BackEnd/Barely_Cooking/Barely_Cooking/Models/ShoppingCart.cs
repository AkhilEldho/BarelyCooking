using System.ComponentModel.DataAnnotations.Schema;

namespace Barely_Cooking_API.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public string UserId { get; set; }

        public ICollection<Cart> Cart { get; set; }

        [NotMapped]
        public double CartTotal { get; set; }
        [NotMapped]
        public string StripePaymentIntedId { get; set; }
        [NotMapped]
        public string ClientSecret { get; set; }

    }
}
