using BusinessObjects.Models;
using DataAccessObjects.DTOs.HealthCertificateDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCertificateController : ControllerBase
    {
        private readonly IHealthCertificateService _healthCertificateService;

        public HealthCertificateController(IHealthCertificateService healthCertificateService)
        {
            _healthCertificateService = healthCertificateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHealthCertificates()
        {
            var healthCertificate = await _healthCertificateService.GetAllHealthCertificates();
            return Ok(healthCertificate);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetHealthCertificateById(int Id)
        {
            var healthCertificate = await _healthCertificateService.GetHealthCertificateById(Id);
            return Ok(healthCertificate);
        }

        [HttpPost]
        public async Task<ActionResult<HealthCertificate>> CreateHealthCertificate(CreateHealthCertificateDTO createHealthCertificateDTO)
        {
            var healthCertificate = await _healthCertificateService.CreateHealthCertificate(createHealthCertificateDTO);
            return Ok(healthCertificate);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateHealthCertificate(int Id, UpdateHealthCertificateDTO updateHealthCertificateDTO)
        {
            var healthCertificate = await _healthCertificateService.UpdateHealthCertificate(Id, updateHealthCertificateDTO);
            return Ok(healthCertificate);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreHealthcertificate(int Id)
        {
            await _healthCertificateService.RestoreHealthCertificate(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteHealthCertificate(int Id)
        {
            var healthCertificate = await _healthCertificateService.DeleteHealthCertificate(Id);
            return Ok();
        }
    }
}
