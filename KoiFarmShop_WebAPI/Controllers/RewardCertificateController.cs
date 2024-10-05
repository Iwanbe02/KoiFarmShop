using BusinessObjects.Models;
using DataAccessObjects.DTOs.RewardCertificateDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardCertificateController : ControllerBase
    {
        private readonly IRewardCertificateService _rewardCertificateService;

        public RewardCertificateController(IRewardCertificateService rewardCertificateService)
        {
            _rewardCertificateService = rewardCertificateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRewardCertificates()
        {
            var rewardCertificate = await _rewardCertificateService.GetAllRewardCertificates();
            return Ok(rewardCertificate);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRewardCertificateById(int Id)
        {
            var rewardCertificate = await _rewardCertificateService.GetRewardCertificateById(Id);
            return Ok(rewardCertificate);
        }

        [HttpPost]
        public async Task<ActionResult<RewardCertificate>> CreateRewardCertificate(CreateRewardCertificateDTO createRewardCertificateDTO)
        {
            var rewardCertificate = await _rewardCertificateService.CreateRewardCertificate(createRewardCertificateDTO);
            return Ok(rewardCertificate);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateRewardCertificate(int Id, UpdateRewardCertificateDTO updateRewardCertificateDTO)
        {
            var rewardCertificate = await _rewardCertificateService.UpdateRewardCertificate(Id, updateRewardCertificateDTO);
            return Ok(rewardCertificate);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreRewardCertificate(int Id)
        {
            await _rewardCertificateService.RestoreRewardCertificate(Id);
            return(Ok());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRewardCertificate(int Id)
        {
            var rewardCertificate = await _rewardCertificateService.DeleteRewardCertificate(Id);
            return Ok(rewardCertificate);
        }
    }
}
