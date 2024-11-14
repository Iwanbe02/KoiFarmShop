using BusinessObjects.Models;
using DataAccessObjects.DTOs.CartDTO;
using DataAccessObjects.DTOs.CategoryDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Implement;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            var cart = await _cartService.GetAllCarts();
            return Ok(cart);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCartById(int Id)
        {
            var cart = await _cartService.GetCartById(Id);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateCart(CreateCartDTO createCartDTO)
        {
            var cart = await _cartService.CreateCart(createCartDTO);
            return Ok(cart);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCart(int Id, UpdateCartDTO updateCartDTO)
        {
            var cart = await _cartService.UpdateCart(Id, updateCartDTO);
            return Ok(cart);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreCart(int Id)
        {
            await _cartService.RestoreCart(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCart(int Id)
        {
            var cart = await _cartService.DeleteCart(Id);
            return Ok();
        }
    }
}
