using BusinessObjects.Models;
using DataAccessObjects.DTOs.KoiFishDTO;
using DataAccessObjects.DTOs.KoiFishyDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoiFishyController : ControllerBase
    {
        private readonly IKoiFishyService _koiFishyService;
        public KoiFishyController(IKoiFishyService koiFishyService)
        {
            _koiFishyService = koiFishyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKoiFishes()
        {
            var koi = await _koiFishyService.GetAllKoiFishes();
            return Ok(koi);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetKoiFishyById(int Id)
        {
            var koi = await _koiFishyService.GetKoiFishyById(Id);
            return Ok(koi);
        }

        [HttpPost]
        public async Task<ActionResult<KoiFishy>> CreateKoiFishy(CreateKoiFishyDTO createKoiFishyDTO)
        {
            var koi = await _koiFishyService.CreateKoiFishy(createKoiFishyDTO);
            return Ok(koi);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateKoiFish(int Id, UpdateKoiFishyDTO updateKoiFishyDTO)
        {
            var koi = await _koiFishyService.UpdateKoiFishy(Id, updateKoiFishyDTO);
            return Ok(koi);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> DeleteOrEnable(int Id)
        {
            await _koiFishyService.RestoreKoiFishy(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteKoiFish(int Id)
        {
            var koi = await _koiFishyService.DeleteKoiFishy(Id);
            return Ok();
        }
    }
}
