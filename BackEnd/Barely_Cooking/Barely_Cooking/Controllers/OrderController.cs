using Barely_Cooking_API.Data;
using Barely_Cooking_API.Models.DTO;
using Barely_Cooking_API.Models;
using Barely_Cooking_API.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Barely_Cooking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ApiResponse _apiResponse;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
            _apiResponse = new ApiResponse();
        }

        //Get all order based on userId
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetOrders(string? userId)
        {
            try
            {
                var orders = _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Cake).OrderByDescending(x => x.OrderId);

                if (!string.IsNullOrEmpty(userId))
                {
                    _apiResponse.Result = orders.Where(x => x.ApplicationUserId == userId).ToList();
                }
                else
                {
                    _apiResponse.Result = orders;
                }

                _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _apiResponse;
        }

        [HttpGet("{orderId:int}")]
        public async Task<ActionResult<ApiResponse>> GetOrder(int orderId)
        {
            try
            {
                if (orderId == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }

                var orders = _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Cake).Where(x => x.OrderId == orderId);

                if (orders == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }

                _apiResponse.Result = orders;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _apiResponse;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateOrder([FromBody] OrderCreateDTO orderCreateDTO)
        {
            try
            {
                Order order = new()
                {
                    ApplicationUserId = orderCreateDTO.ApplicationUserId,
                    PickupEmail = orderCreateDTO.PickupEmail,
                    PickupName = orderCreateDTO.PickupName,
                    PickupNumber = orderCreateDTO.PickupNumber,
                    OrderTotal = orderCreateDTO.OrderTotal,
                    OrderDate = DateTime.Now,
                    StripePaymentIntentId = orderCreateDTO.StripePaymentIntentId,
                    TotalItems = orderCreateDTO.TotalItems,
                    Status = String.IsNullOrEmpty(orderCreateDTO.Status) ? SD.status_Pending : orderCreateDTO.Status,
                };

                if (ModelState.IsValid)
                {
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    foreach (var orderDetailsDTO in orderCreateDTO.OrderDetailsCreateDTO)
                    {
                        OrderDetail orderDetail = new()
                        {
                            OrderId = order.OrderId,
                            CakeName = orderDetailsDTO.CakeName,
                            CakeId = orderDetailsDTO.CakeId,
                            Price = orderDetailsDTO.Price,
                            Quantity = orderDetailsDTO.Quantity,
                        };

                        _context.OrderDetails.Add(orderDetail);
                    }
                    _context.SaveChanges();

                    _apiResponse.Result = order;
                    order.OrderDetails = null;
                    _apiResponse.StatusCode = HttpStatusCode.Created;

                    return Ok(_apiResponse);
                }
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _apiResponse;
        }

        [HttpPut("{orderId:int}")]
        public async Task<ActionResult<ApiResponse>> UpdateOrder(int orderId, [FromBody] OrderUpdateDTO orderUpdateDTO)
        {
            try
            {
                if (orderUpdateDTO == null || orderId != orderUpdateDTO.OrderId)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest();
                }

                Order order = _context.Orders.FirstOrDefault(x => x.OrderId == orderId);

                if (order == null)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest();
                }

                if (!string.IsNullOrEmpty(orderUpdateDTO.PickupName))
                {
                    order.PickupName = orderUpdateDTO.PickupName;
                }
                if (!string.IsNullOrEmpty(orderUpdateDTO.PickupNumber))
                {
                    order.PickupNumber = orderUpdateDTO.PickupNumber;
                }
                if (!string.IsNullOrEmpty(orderUpdateDTO.PickupEmail))
                {
                    order.PickupEmail = orderUpdateDTO.PickupEmail;
                }
                if (!string.IsNullOrEmpty(orderUpdateDTO.Status))
                {
                    order.Status = orderUpdateDTO.Status;
                }
                if (!string.IsNullOrEmpty(orderUpdateDTO.StripePaymentIntentId))
                {
                    order.StripePaymentIntentId = orderUpdateDTO.StripePaymentIntentId;
                }

                _context.SaveChanges();
                _apiResponse.StatusCode = HttpStatusCode.NoContent;
                _apiResponse.IsSuccess = true;

                return Ok(_apiResponse);


            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _apiResponse;
        }
    }
}
