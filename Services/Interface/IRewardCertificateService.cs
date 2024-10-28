using BusinessObjects.Models;
using DataAccessObjects.DTOs.PromotionDTO;
using DataAccessObjects.DTOs.RewardCertificateDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IRewardCertificateService
    {
        Task<IEnumerable<RewardCertificate>> GetAllRewardCertificates();
        Task<RewardCertificate> GetRewardCertificateById(int id);
        Task<RewardCertificate> CreateRewardCertificate(CreateRewardCertificateDTO createRewardCertificate);
        Task<RewardCertificate> UpdateRewardCertificate(int id, UpdateRewardCertificateDTO updateRewardCertificate);
        Task<RewardCertificate> DeleteRewardCertificate(int id);
        Task<RewardCertificate> RestoreRewardCertificate(int id);
    }
}
