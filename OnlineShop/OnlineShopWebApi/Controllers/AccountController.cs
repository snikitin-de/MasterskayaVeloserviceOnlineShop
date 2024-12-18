using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopWebApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authorization = OnlineShopWebApi.Models.Authorization;
using Registration = OnlineShopWebApi.Models.Registration;

namespace OnlineShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountService _userService;

        public AccountController(IConfiguration configuration, IAccountService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] Authorization data)
        {
            var result = await _userService.HasAuthenticationPassedAsync(data);

            if (result)
            {
                var tokenString = GenerateJwtToken(data.Login);

                return Ok(new { Token = tokenString, Message = "Success" }); ;
            }

            return BadRequest("Пожалуйста, укажите валидный логин и пароль");
        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] Registration data)
        {
            var result = await _userService.HasRegistrationPassedAsync(data);

            if (result)
            {
                var tokenString = GenerateJwtToken(data.Email);

                return Ok(new { Token = tokenString, Message = "Success" }); ;
            }

            return BadRequest("Пожалуйста, укажите валидный email и пароль");
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetResult))]
        public IActionResult GetResult()
        {
            return Ok("API Validated");
        }

        private string GenerateJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([new Claim("email", userName)]),
                Expires = DateTime.UtcNow.AddHours(24),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
