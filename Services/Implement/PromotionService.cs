using BusinessObjects.Enums;
using BusinessObjects.Models;
using DataAccessObjects.DTOs.PromotionDTO;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<Promotion> CreatePromotion(CreatePromotionDTO createPromotion)
        {
            var promotion = new Promotion
            {
                Point = createPromotion.Point,
                DiscountPercentage = createPromotion.DiscountPercentage,
                Status = CertificateStatus.Valid.ToString(),
                CreatedDate = DateTime.Now
            };
            await _promotionRepository.AddAsync(promotion);
            return promotion;
        }

        public async Task<Promotion> DeletePromotion(int id)
        {
            var promotion = await _promotionRepository.GetByIdAsync(id);
            if(promotion == null)
            {
                throw new Exception($"Promotion with ID{id} is not found");
            }
            promotion.IsDeleted = true;
            promotion.DeletedDate = DateTime.Now;
            await _promotionRepository.UpdateAsync(promotion);
            return promotion;
        }

        public async Task<IEnumerable<Promotion>> GetAllPromotions()
        {
            return await _promotionRepository.GetAllAsync();
        }

        public async Task<Promotion> GetPromotionById(int id)
        {
            return await _promotionRepository.GetByIdAsync(id);
        }

        public async Task<Promotion> RestorePromotion(int id)
        {
            var promotion = await _promotionRepository.GetByIdAsync(id);
            if (promotion == null)
            {
                throw new Exception($"Promotion with ID{id} is not found");
            }
            if (promotion.IsDeleted == true)
            {
                promotion.IsDeleted = false;
                await _promotionRepository.UpdateAsync(promotion) ;
            }
            return promotion;
        }

        public async Task<Promotion> UpdatePromotion(int id, UpdatePromotionDTO updatePromotion)
        {
            var promotion = await _promotionRepository.GetByIdAsync(id);
            if (promotion == null)
            {
                throw new Exception($"Promotion with ID{id} is not found");
            }
            promotion.Point = updatePromotion.Point;
            promotion.DiscountPercentage = updatePromotion.DiscountPercentage;
            promotion.Status = updatePromotion.Status;
            promotion.ModifiedDate = DateTime.Now;

            await _promotionRepository.UpdateAsync(promotion);
            return promotion;
        }
    }


}
