using BusinessObjects.Models;
using DataAccessObjects.DTOs.CartDTO;
using DataAccessObjects.DTOs.CartItemDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Implement;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;
        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartItems()
        {
            var cart = await _cartItemService.GetAllCartItems();
            return Ok(cart);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCartById(int Id)
        {
            var cart = await _cartItemService.GetCartItemById(Id);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<CartItem>> CreateCartItem(CreateCartItemDTO createCartItemDTO)
        {
            var cart = await _cartItemService.CreateCartItem(createCartItemDTO);
            return Ok(cart);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCartItem(int Id, UpdateCartItemDTO updateCartItemDTO)
        {
            var cart = await _cartItemService.UpdateCartItem(Id, updateCartItemDTO);
            return Ok(cart);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreCartItem(int Id)
        {
            await _cartItemService.RestoreCartItem(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCartItem(int Id)
        {
            var cart = await _cartItemService.DeleteCartItem(Id);
            return Ok();
        }
    }
}
