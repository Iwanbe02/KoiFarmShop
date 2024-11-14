using BusinessObjects.Models;
using DataAccessObjects.DTOs.FeedbackDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacks();
        Task<Feedback> GetFeedbackById(int id);
        Task<Feedback> CreateFeedback(CreateFeedbackDTO createFeedback);
        Task<Feedback> UpdateFeedback(int id, UpdateFeedbackDTO updateFeedback);
        Task<Feedback> DeleteFeedback(int id);
        Task<Feedback> RestoreFeedback(int id);
    }
}
