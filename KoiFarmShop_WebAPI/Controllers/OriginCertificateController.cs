using BusinessObjects.Models;
using DataAccessObjects.DTOs.OrriginCetificateDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Implement;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OriginCertificateController : ControllerBase
    {
        private readonly IOriginCertificateService _origincertificateService;

        public OriginCertificateController(IOriginCertificateService origincertificateService)
        {
            _origincertificateService = origincertificateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOriginCertificates()
        {
            var origincertificate = await _origincertificateService.GetAllOriginCertificate(); ;
            return Ok(origincertificate);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOriginCertificateById(int Id)
        {
            var originCertificate = await _origincertificateService.GetOriginCertificateById(Id);
            return Ok(originCertificate);
        }

        [HttpPost]
        public async Task<ActionResult<OriginCertificate>> CreateOriginCertificate(CreateOriginCertificateDTO createOriginCertificateDTO)
        {
            var originCertificate = await _origincertificateService.CreateOriginCertificate(createOriginCertificateDTO);
            return Ok(originCertificate);
        }

        [HttpPut("{Id}")]
       public async Task<IActionResult> UpdateOriginCertificate(int Id, UpdateOriginCertificateDTO updateOriginCertificateDTO)
        {
            var originCertificate = await _origincertificateService.UpdateOriginCertificate(Id, updateOriginCertificateDTO);
            return Ok(originCertificate);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreOriginCertificate(int Id)
        {
            await _origincertificateService.RestoreOriginCertificate(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOriginCertificate(int Id)
        {
            var originCertificate = await _origincertificateService.DeleteOriginCertificate(Id);
            return Ok();
            
        }
    }
}
