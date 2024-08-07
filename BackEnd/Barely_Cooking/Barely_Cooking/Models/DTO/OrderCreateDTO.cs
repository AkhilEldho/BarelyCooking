﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Barely_Cooking_API.Models.DTO
{
    public class OrderCreateDTO
    {

        [Required]
        public string PickupName { get; set; }

        [Required]
        public string PickupNumber { get; set; }

        [Required]
        public string PickupEmail { get; set; }

        public string ApplicationUserId { get; set; }
        public double OrderTotal { get; set; }

        public DateTime OrderDate { get; set; }
        public string StripePaymentIntentId { get; set; }
        public string Status { get; set; }
        public int TotalItems { get; set; }

        public IEnumerable<OrderDetailCreateDTO> OrderDetailsCreateDTO { get; set; }
    }
}
