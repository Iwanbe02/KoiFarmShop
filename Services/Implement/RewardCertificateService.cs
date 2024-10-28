using BusinessObjects.Models;
using DataAccessObjects.DTOs.RewardCertificateDTO;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class RewardCertificateService : IRewardCertificateService
    {
        private readonly IRewardCertificateRepository _rewardCertificateRepository;

        public RewardCertificateService(IRewardCertificateRepository rewardCertificateRepository)
        {
            _rewardCertificateRepository = rewardCertificateRepository;
        }

        public async Task<RewardCertificate> CreateRewardCertificate(CreateRewardCertificateDTO createRewardCertificate)
        {
            var rewardCertificate = new RewardCertificate
            {
                KoiId = createRewardCertificate.KoiId,
                OrderId = createRewardCertificate.OrderId,
                Description = createRewardCertificate.Description,
            };
            await _rewardCertificateRepository.AddAsync(rewardCertificate);
            return rewardCertificate;
        }

        public async Task<RewardCertificate> DeleteRewardCertificate(int id)
        {
            var rewardCertificate = await _rewardCertificateRepository.GetByIdAsync(id);
            if(rewardCertificate == null)
            {
                throw new Exception($"RewardCertificate with ID{id} is not found");
            }
            rewardCertificate.IsDeleted = true;
            await _rewardCertificateRepository.UpdateAsync(rewardCertificate);
            return rewardCertificate;
        }

        public async Task<IEnumerable<RewardCertificate>> GetAllRewardCertificates()
        {
            return await _rewardCertificateRepository.GetAllAsync();
        }

        public async Task<RewardCertificate> GetRewardCertificateById(int id)
        {
            return await _rewardCertificateRepository.GetByIdAsync(id);
        }

        public async Task<RewardCertificate> RestoreRewardCertificate(int id)
        {
            var rewardCertificate = await _rewardCertificateRepository.GetByIdAsync(id);
            if (rewardCertificate == null)
            {
                throw new Exception($"RewardCertificate with ID{id} is not found");
            }
            if (rewardCertificate.IsDeleted == true)
            {
                rewardCertificate.IsDeleted = false;
                await _rewardCertificateRepository.UpdateAsync(rewardCertificate);
            }
            return rewardCertificate;

        }

        public async Task<RewardCertificate> UpdateRewardCertificate(int id, UpdateRewardCertificateDTO updateRewardCertificate)
        {
            var rewardCertificate = await _rewardCertificateRepository.GetByIdAsync(id);
            if(rewardCertificate == null)
            {
                throw new Exception($"RewardCertificate with ID{id} is not found");
            }
            rewardCertificate.KoiId = updateRewardCertificate.KoiId;
            rewardCertificate.OrderId = updateRewardCertificate.OrderId;
            rewardCertificate.Description = updateRewardCertificate.Description;

            await _rewardCertificateRepository.UpdateAsync(rewardCertificate);
            return rewardCertificate;
        }
    }
}
