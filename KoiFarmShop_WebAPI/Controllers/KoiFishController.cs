using BusinessObjects.Models;
using DataAccessObjects.DTOs.KoiFishDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KoiFishController : ControllerBase
    {
        private readonly IKoiFishService _koiFishService;
        public KoiFishController(IKoiFishService koiFishService)
        {
            _koiFishService = koiFishService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKoiFishes()
        {
            var koi = await _koiFishService.GetAllKoiFishes();
            return Ok(koi);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetKoiFishById(int Id)
        {
            var koi = await _koiFishService.GetKoiFishById(Id);
            return Ok(koi);
        }

        [HttpPost]
        public async Task<ActionResult<KoiFish>> CreateKoiFish([FromForm] CreateKoiFishDTO createKoiFishDTO)
        {
            var koi = await _koiFishService.CreateKoiFish(createKoiFishDTO);
            return Ok(koi);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateKoiFish(int Id, [FromForm] UpdateKoiFishDTO updateKoiFishDTO)
        {
            var koi = await _koiFishService.UpdateKoiFish(Id, updateKoiFishDTO);
            return Ok(koi);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteKoiFish(int Id)
        {
            var koi = await _koiFishService.DeleteKoiFish(Id);
            return Ok(koi);
        }
    }
}
