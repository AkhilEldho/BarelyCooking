using Barely_Cooking_API.Data;
using Barely_Cooking_API.Models;
using Barely_Cooking_API.Models.DTO;
using Barely_Cooking_API.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Barely_Cooking_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ApiResponse _apiResponse;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretKey;

        public AuthenticationController(ApplicationDbContext context, IConfiguration configuration,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _apiResponse = new ApiResponse();
            _userManager = userManager;
            _roleManager = roleManager;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        //Registering User
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            ApplicationUser applicationUser = _context.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == model.Username.ToLower());

            if (applicationUser != null)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Username already exists");

                return BadRequest(_apiResponse);
            }

            ApplicationUser newUser = new()
            {
                UserName = model.Username,
                Email = model.Username,
                NormalizedEmail = model.Username.ToUpper(),
                Name = model.Name
            };

            try
            {
                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
                    }

                    if (model.Role.ToLower() == SD.Role_Admin)
                    {
                        await _userManager.AddToRoleAsync(newUser, SD.Role_Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser, SD.Role_Customer);
                    }

                    _apiResponse.StatusCode = HttpStatusCode.OK;
                    _apiResponse.IsSuccess = true;

                    return Ok(_apiResponse);
                }
            }
            catch (Exception ex)
            {
                _apiResponse.ErrorMessages.Add(ex.Message);
            }

            _apiResponse.StatusCode = HttpStatusCode.BadRequest;
            _apiResponse.IsSuccess = false;
            _apiResponse.ErrorMessages.Add("An error occured while registering");

            return BadRequest(_apiResponse);

        }

        //User Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            ApplicationUser applicationUser = _context.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == model.Username.ToLower());

            if (applicationUser == null)
            {
                _apiResponse.Result = new LoginRequestDTO();
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("User not found");

                return BadRequest(_apiResponse);
            }

            bool isValid = await _userManager.CheckPasswordAsync(applicationUser, model.Password);

            if (!isValid)
            {
                _apiResponse.Result = new LoginRequestDTO();
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Check Password");

                return BadRequest(_apiResponse);
            }

            //Generating JWT Token
            var roles = await _userManager.GetRolesAsync(applicationUser);
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("fullName", applicationUser.Name),
                    new Claim("id", applicationUser.Id),
                    new Claim(ClaimTypes.Email, applicationUser.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);


            LoginResponseDTO loginResponseDTO = new()
            {
                Email = applicationUser.Email,
                Token = tokenHandler.WriteToken(token)
            };

            if (loginResponseDTO == null || string.IsNullOrEmpty(loginResponseDTO.Token))
            {
                _apiResponse.Result = new LoginRequestDTO();
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Username or password is inncorrect");
            }

            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            _apiResponse.Result = loginResponseDTO;

            return Ok(_apiResponse);

        }

    }
}
