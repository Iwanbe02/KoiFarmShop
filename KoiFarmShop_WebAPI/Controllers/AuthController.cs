using BusinessObjects.ReqDto;
using DataAccessObjects.DTOs.AccountDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userService;
        private readonly IAccountService _accountService;

        public AuthController(IUserServices userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
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
        public IActionResult Login([FromBody] LoginDTO model)
        {
            try
            {
                var token = _userService.Login(model.Email, model.Password);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

    }
}
