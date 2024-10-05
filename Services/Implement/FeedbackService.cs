using BusinessObjects.Models;
using DataAccessObjects.DTOs.FeedbackDTO;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class FeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<Feedback> CreateFeedback(CreateFeedbackDTO createFeedback)
        {
            var feedback = new Feedback
            {
                AccountId = createFeedback.AccountId,
                Status = createFeedback.Status,
                Description = createFeedback.Description,
                Rating = createFeedback.Rating,
            };
            await _feedbackRepository.AddAsync(feedback);
            return feedback;
        }

        public async Task<Feedback> DeleteFeedback(int id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null)
            {
                throw new Exception($"Feedback with ID{id} is not found");
            }
            feedback.IsDeleted = true;
            await _feedbackRepository.UpdateAsync(feedback);
            return feedback;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedback()
        {
            return await _feedbackRepository.GetAllAsync();
        }

        public async Task<Feedback> GetFeedbackById(int id)
        {
            return await _feedbackRepository.GetByIdAsync(id);
        }

        public async Task<Feedback> RestoreFeedback(int id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null)
            {
                throw new Exception($"Feedback with ID{id} is not found");
            }
            if (feedback.IsDeleted == true)
            {
                feedback.IsDeleted = false;
                await _feedbackRepository.UpdateAsync(feedback);
            }
            return feedback;
        }

        public async Task<Feedback> UpdateFeedback(int id, UpdateFeedbackDTO updateFeedback)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if(feedback == null)
            {
                throw new Exception($"Feedback with ID{id} is not found");
            }
            feedback.AccountId = updateFeedback.AccountId;
            feedback.Status = updateFeedback.Status;
            feedback.Description = updateFeedback.Description;
            feedback.Rating = updateFeedback.Rating;

            await _feedbackRepository.UpdateAsync(feedback);
            return feedback;
        }
    }
}
