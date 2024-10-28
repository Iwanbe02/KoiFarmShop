using BusinessObjects.Models;
using DataAccessObjects.DTOs.FeedbackDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var feedback = await _feedbackService.GetAllFeedbacks();
            return Ok(feedback);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetfeedbackById(int Id)
        {
            var feedback = await _feedbackService.GetFeedbackById(Id);
            return Ok(feedback);
        }

        [HttpPost]
        public async Task<ActionResult<Feedback>> CreateFeedback(CreateFeedbackDTO createFeedback)
        {
            var feedback = await _feedbackService.CreateFeedback(createFeedback);
            return Ok(feedback);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateFeedback (int Id, UpdateFeedbackDTO updateFeedback)
        {
            var feedback = await _feedbackService.UpdateFeedback(Id, updateFeedback);
            return Ok(feedback);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreFeedback(int Id)
        {
            await _feedbackService.RestoreFeedback(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFeedback(int Id)
        {
            var feedback = await _feedbackService.DeleteFeedback(Id);
            return Ok(feedback);
        }
    }
}
