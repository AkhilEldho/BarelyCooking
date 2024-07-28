using Barely_Cooking_API.Data;
using Barely_Cooking_API.Models;
using Barely_Cooking_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Barely_Cooking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentingCartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ApiResponse _apiResponse;

        public RentingCartController(ApplicationDbContext context, ImageBlobService imageBlobService)
        {
            _context = context;
            _apiResponse = new ApiResponse();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetShoppingCart(string userId)
        {
            try
            {
                ShoppingCart cart;

                if (string.IsNullOrEmpty(userId))
                {
                    cart = new ShoppingCart();
                }
                else
                {
                    cart = _context.ShoppingCarts.Include(x => x.Cart).ThenInclude(x => x.Cake).FirstOrDefault(x => x.UserId == userId);
                }

                if (cart.Cart != null && cart.Cart.Count > 0)
                {
                    cart.CartTotal = cart.Cart.Sum(x => x.Quantity * x.Cake.Price);
                }

                _apiResponse.Result = cart;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
            }

            return _apiResponse;
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddorUpdateCart(string userId, int cakeId, int quantity)
        {
            ShoppingCart shoppingCart = _context.ShoppingCarts.Include(x => x.Cart).ThenInclude(x => x.Cake).FirstOrDefault(x => x.UserId == userId);
            Cake cake = _context.Cakes.FirstOrDefault(m => m.CakeId == cakeId);

            if (cake == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }

            if (shoppingCart == null && quantity > 0)
            {
                //Create new cart and add the vehicle customer has chosen
                ShoppingCart newCart = new() { UserId = userId };
                _context.ShoppingCarts.Add(newCart);
                _context.SaveChanges();

                Cart newCartItem = new()
                {
                    CakeId = cakeId,
                    Quantity = quantity,
                    ShoppingCartId = newCart.ShoppingCartId,
                    Cake = null
                };

                _context.Carts.Add(newCartItem);
                _context.SaveChanges();

            }
            else
            {
                Cart cartItemCheck = shoppingCart.Cart.FirstOrDefault(x => x.CakeId == cakeId);

                //Checking if user already has a cart 
                if (cartItemCheck == null && quantity > 0)
                {
                    //Item does not exist create new cart
                    Cart newCartItem = new()
                    {
                        CakeId = cakeId,
                        Quantity = quantity,
                        ShoppingCartId = shoppingCart.ShoppingCartId,
                        Cake = null
                    };

                    _context.Carts.Add(newCartItem);
                    _context.SaveChanges();
                }
                else
                {
                    //Update quantity
                    int quantityUpate = cartItemCheck.Quantity + quantity;

                    if (quantityUpate == 0 || quantityUpate <= 0)
                    {
                        //remove item
                        _context.Carts.Remove(cartItemCheck);
                        if (shoppingCart.Cart.Count() == 1)
                        {
                            _context.ShoppingCarts.Remove(shoppingCart);
                        }

                        _context.SaveChanges();
                    }
                    else
                    {
                        cartItemCheck.Quantity = quantityUpate;
                        _context.SaveChanges();
                    }
                }
            }

            return _apiResponse;
        }

    }
}
