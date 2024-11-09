using BusinessObjects.Models;
using BusinessObjects.ReqDto;
using DataAccessObjects.DTOs;
using DataAccessObjects.DTOs.AccountDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userService;
        private readonly IAccountService _accountService;
        private readonly IConfiguration _config;

        public AuthController(IUserServices userService, IAccountService accountService, IConfiguration config)
        {
            _userService = userService;
            _accountService = accountService;
            _config = config;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterReqDto model)
        {
            try
            {
                _userService.Register(model);
                return Ok(new { Message = "Registration successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Model.LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "Invalid client request",
                    Data = null,
                    RoleId = 0
                });
            }
            var account = await this._accountService.Login(request.Email, request.Password);
            if (account == null)
            {
                return Unauthorized(new ApiResponse
                {
                    StatusCode = 401,
                    Message = "Unauthorized",
                    Data = null,
                    RoleId = 0
                });
            }

            var token = this.GenerateJSONWebToken(account);
            var user = await _accountService.GetAccountById(account.Id);
            var roleId = user.RoleId;
            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Login successful",
                Data = token,
                RoleId = (int)roleId,
                UserId = account.Id
            });
        }

        private string GenerateJSONWebToken(Account userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              new Claim[]
              {
              new(ClaimTypes.Email, userInfo.Email),
              new(ClaimTypes.Role, userInfo.RoleId.ToString()),
              new("userId", userInfo.Id.ToString()),
              },
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
