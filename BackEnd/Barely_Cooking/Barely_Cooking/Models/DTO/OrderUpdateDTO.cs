using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Barely_Cooking_API.Models.DTO
{
    public class OrderUpdateDTO
    {
        public int OrderId { get; set; }
        public string PickupName { get; set; }
        public string PickupNumber { get; set; }
        public string PickupEmail { get; set; }

        public DateTime OrderDate { get; set; }
        public string StripePaymentIntentId { get; set; }
        public string Status { get; set; }

    }
}
