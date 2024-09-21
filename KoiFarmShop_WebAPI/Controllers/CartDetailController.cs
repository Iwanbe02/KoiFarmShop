using BusinessObjects.Models;
using DataAccessObjects.DTOs.CartDetailDTO;
using DataAccessObjects.DTOs.CartDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Implement;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private readonly ICartDetailService _cartDetailService;
        public CartDetailController(ICartDetailService cartDetailService)
        {
            _cartDetailService = cartDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartDetails()
        {
            var cartDetail = await _cartDetailService.GetAllCartDetails();
            return Ok(cartDetail);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCartDetailById(int Id)
        {
            var cartDetail = await _cartDetailService.GetCartDetailById(Id);
            return Ok(cartDetail);
        }

        [HttpPost]
        public async Task<ActionResult<CartDetail>> CreateCartDetail(CreateCartDetailDTO createCartDetailDTO)
        {
            var cartDetail = await _cartDetailService.CreateCartDetail(createCartDetailDTO);
            return Ok(cartDetail);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCartDetail(int Id, UpdateCartDetailDTO updateCartDetailDTO)
        {
            var cartDetail = await _cartDetailService.UpdateCartDetail(Id, updateCartDetailDTO);
            return Ok(cartDetail);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreCartDetail(int Id)
        {
            await _cartDetailService.RestoreCartDetail(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCartDetail(int Id)
        {
            var cartDetail = await _cartDetailService.DeleteCartDetail(Id);
            return Ok();
        }
    }
}
