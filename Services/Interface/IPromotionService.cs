using BusinessObjects.Models;
using DataAccessObjects.DTOs.PaymentDTO;
using DataAccessObjects.DTOs.PromotionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetAllPromotions();
        Task<Promotion> GetPromotionById(int id);
        Task<Promotion> CreatePromotion(CreatePromotionDTO createPromotion);
        Task<Promotion> UpdatePromotion(int id, UpdatePromotionDTO updatePromotion);
        Task<Promotion> DeletePromotion(int id);
        Task<Promotion> RestorePromotion(int id);
    }
}
