using BusinessObjects.Enums;
using BusinessObjects.Models;
using DataAccessObjects.DTOs.HealthCertificateDTO;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class HealthCertificateService : IHealthCertificateService
    {
       private readonly IHealthCertificateRepository _healthCertificaterepository;

       public HealthCertificateService(IHealthCertificateRepository healthCertificateRepository)
        {
            _healthCertificaterepository = healthCertificateRepository;
        }

        public async Task<HealthCertificate> CreateHealthCertificate(CreateHealthCertificateDTO createHealthCertificate)
        {
            var healthCertificate = new HealthCertificate
            {
                KoiId = createHealthCertificate.KoiId,
                OrderId = createHealthCertificate.OrderId,
                Status = CertificateStatus.Valid.ToString(),
                StartTime = createHealthCertificate.StartTime,
                EndTime = createHealthCertificate.EndTime,
                CreatedDate = DateTime.Now
            };
            await _healthCertificaterepository.AddAsync(healthCertificate);
            return healthCertificate;
        }

        public async Task<HealthCertificate> DeleteHealthCertificate(int id)
        {
            var healthCertificate = await _healthCertificaterepository.GetByIdAsync(id);
            if(healthCertificate == null)
            {
                throw new Exception($"HealthCertificate with ID{id} is not found");
            }
            healthCertificate.IsDeleted = true;
            healthCertificate.DeletedDate = DateTime.Now;
            await _healthCertificaterepository.UpdateAsync(healthCertificate);
            return healthCertificate;
        }

        public async Task<IEnumerable<HealthCertificate>> GetAllHealthCertificates()
        {
            return await _healthCertificaterepository.GetAllAsync();
        }

        public async Task<HealthCertificate> GetHealthCertificateById(int id)
        {
            return await _healthCertificaterepository.GetByIdAsync(id);
        }

        public async Task<HealthCertificate> RestoreHealthCertificate(int id)
        {
            var healthCertificate = await GetHealthCertificateById(id);
            if (healthCertificate == null)
            {
                throw new Exception($"HealthCertificate with ID{id} is not found");
            }
            if (healthCertificate.IsDeleted == true)
            {
                healthCertificate.IsDeleted = true;
                await _healthCertificaterepository.UpdateAsync(healthCertificate);
            }
            return healthCertificate;
        }

        public async Task<HealthCertificate> UpdateHealthCertificate(int id, UpdateHealthCertificateDTO updatehealthCertificate)
        {
            var healthCertificate = await _healthCertificaterepository.GetByIdAsync(id);
            if (updatehealthCertificate == null )
            {
                throw new Exception($"HealthCertificate with ID{id} is not found");
            }
            healthCertificate.Status = updatehealthCertificate.Status;
            healthCertificate.StartTime = updatehealthCertificate.StartTime;
            healthCertificate.EndTime = updatehealthCertificate.EndTime;

            await _healthCertificaterepository.UpdateAsync(healthCertificate);
            return healthCertificate;
        }
    }
}
