using BusinessObjects.Models;
using DataAccessObjects.DTOs.PromotionDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPromotions()
        {
            var promotion = await _promotionService.GetAllPromotions();
            return Ok(promotion);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPromotionById(int Id)
        {
            var promotion = await _promotionService.GetPromotionById(Id);
            return Ok(promotion);
        }

        [HttpPost]
        public async Task<ActionResult<Promotion>> CreatePromotion(CreatePromotionDTO createPromotionDTO)
        {
            var promotion = await _promotionService.CreatePromotion(createPromotionDTO);
            return Ok(promotion);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdatePromotion(int Id, UpdatePromotionDTO updatePromotionDTO)
        {
            var promotion = await _promotionService.UpdatePromotion(Id, updatePromotionDTO); ;
            return Ok(promotion);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestorePromotion(int Id)
        {
            await _promotionService.RestorePromotion(Id); 
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePromotion(int Id)
        {
            var promotion = await _promotionService.DeletePromotion(Id);
            return Ok();
        }
    }
}
