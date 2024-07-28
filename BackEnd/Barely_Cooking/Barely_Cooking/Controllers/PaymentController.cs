using Barely_Cooking_API.Data;
using Barely_Cooking_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Net;

namespace Barely_Cooking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        protected ApiResponse _apiResponse;
        public readonly IConfiguration _configuration;

        public PaymentController(ApplicationDbContext context, IConfiguration iConfiguartion)
        {
            _context = context;
            _configuration = iConfiguartion;
            _apiResponse = new ApiResponse();
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> MakePayment(string userId)
        {
            ShoppingCart cart = _context.ShoppingCarts.Include(x => x.Cart).ThenInclude(x => x.Cake).FirstOrDefault(x => x.UserId == userId);

            if (cart == null || cart.Cart == null || cart.Cart.Count() == 0)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;

                return BadRequest(_apiResponse);
            }

            #region Create Payment Intent

            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            cart.CartTotal = cart.Cart.Sum(x => x.Quantity * x.Cake.Price);

            PaymentIntentCreateOptions payment = new()
            {
                Amount = (int)(cart.CartTotal * 100),
                Currency = "nzd",
                PaymentMethodTypes = new List<string>
                {
                    "card"
                }
            };

            PaymentIntentService paymentService = new();
            PaymentIntent paymentIntent = paymentService.Create(payment);

            cart.StripePaymentIntedId = paymentIntent.Id;
            cart.ClientSecret = paymentIntent.ClientSecret;

            #endregion

            _apiResponse.Result = cart;
            _apiResponse.StatusCode = HttpStatusCode.OK;

            return Ok(_apiResponse);
        }

    }
}
