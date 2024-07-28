using Barely_Cooking_API.Data;
using Barely_Cooking_API.Models;
using Barely_Cooking_API.Models.DTO;
using Barely_Cooking_API.Services;
using Barely_Cooking_API.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Barely_Cooking_API.Controllers
{
    [Route("api/Cake")]
    [ApiController] 
    public class MenuItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ApiResponse _apiResponse;
        private readonly ImageBlobService _imageBlobService;

        public MenuItemController(ApplicationDbContext context, ImageBlobService imageBlobService)
        {
            _context = context;
            _imageBlobService = imageBlobService;
            _apiResponse = new ApiResponse();
        }

        //Get All Items
        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            _apiResponse.Result = _context.Cakes;
            _apiResponse.StatusCode = HttpStatusCode.OK;

            return Ok(_apiResponse);
        }

        //Get 1 Item 
        [HttpGet("{cakeId:int}", Name = "GetCake")]
        public async Task<IActionResult> GetCake(int cakeId)
        {
            if (cakeId == 0)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }

            Cake cake = _context.Cakes.FirstOrDefault(x => x.CakeId == cakeId);

            if (cake == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.NotFound;
                _apiResponse.IsSuccess = false;
                return NotFound(_apiResponse);
            }
            else
            {
                _apiResponse.Result = cake;
                _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
        }

        //Creating New Object For Menu & Adding To DB
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateCake([FromForm] CakeCreateDTO cakeCreateDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (cakeCreateDTO.ImageFile == null || cakeCreateDTO.ImageFile.Length == 0)
                    {
                        _apiResponse.IsSuccess = false;
                        return BadRequest();
                    }

                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(cakeCreateDTO.ImageFile.FileName)}";

                    Cake menuItemToCreate = new()
                    {
                        Name = cakeCreateDTO.Name,
                        Description = cakeCreateDTO.Description,
                        SpecialTags = cakeCreateDTO.SpecialTags,
                        Category = cakeCreateDTO.Category,
                        Price = cakeCreateDTO.Price,
                        ImageFile = await _imageBlobService.UploadBlob(fileName, SD.SD_Storage_Container, cakeCreateDTO.ImageFile)
                    };

                    _context.Cakes.Add(menuItemToCreate);
                    _context.SaveChanges();

                    _apiResponse.Result = menuItemToCreate;
                    _apiResponse.StatusCode = HttpStatusCode.Created;

                    return CreatedAtRoute("GetCake", new { cakeId = menuItemToCreate.CakeId }, _apiResponse);
                }
                else
                {
                    _apiResponse.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _apiResponse;
        }

        //Update Object For Menu & Adding To DB
        [HttpPut("{id:int}", Name = "UpdateCake")]
        public async Task<ActionResult<ApiResponse>> UpdateCake(int id, [FromForm] CakeUpdateDTO cakeUpdateDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (cakeUpdateDTO == null || id != cakeUpdateDTO.CakeId)
                    {
                        _apiResponse.IsSuccess = false;
                        return BadRequest();
                    }

                    Cake updateCake = await _context.Cakes.FindAsync(id);

                    if (updateCake == null)
                    {
                        _apiResponse.IsSuccess = false;
                        return BadRequest();
                    }

                    updateCake.Name = cakeUpdateDTO.Name;
                    updateCake.Category = cakeUpdateDTO.Category;
                    updateCake.Description = cakeUpdateDTO.Description;
                    updateCake.SpecialTags= cakeUpdateDTO.SpecialTags;
                    updateCake.Price = cakeUpdateDTO.Price;

                    if (updateCake.ImageFile != null && updateCake.ImageFile.Length > 0)
                    {
                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(cakeUpdateDTO.ImageFile.FileName)}";
                        await _imageBlobService.DeleteBlob(updateCake.ImageFile.Split('/').Last(), SD.SD_Storage_Container);
                        updateCake.ImageFile = await _imageBlobService.UploadBlob(fileName, SD.SD_Storage_Container, cakeUpdateDTO.ImageFile);
                    }

                    _context.Cakes.Update(updateCake);
                    _context.SaveChanges();

                    _apiResponse.StatusCode = HttpStatusCode.NoContent;

                    return Ok(_apiResponse);
                }
                else
                {
                    _apiResponse.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _apiResponse;
        }

        //Update Object For Menu & Adding To DB
        [HttpDelete("{id:int}", Name = "DeleteCake")]
        public async Task<ActionResult<ApiResponse>> DeleteCake(int id)
        {
            try
            {
                Cake updateCake = await _context.Cakes.FindAsync(id);

                if (updateCake == null)
                {
                    _apiResponse.IsSuccess = false;
                    return BadRequest();
                }

                await _imageBlobService.DeleteBlob(updateCake.ImageFile.Split('/').Last(), SD.SD_Storage_Container);
                int delay = 2000;
                Thread.Sleep(delay);

                _context.Cakes.Remove(updateCake);
                _context.SaveChanges();

                _apiResponse.StatusCode = HttpStatusCode.NoContent;

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

