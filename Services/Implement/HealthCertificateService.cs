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
        private readonly IHealthCertificateRepository _healthCertificateRepository;

       public HealthCertificateService(IHealthCertificateRepository healthCertificateRepository)
        {
            _healthCertificateRepository = healthCertificateRepository;
        }

        public Task<HealthCertificate> CreateHealthCertificate(CreateHealthCertificateDTO healthCertificate)
        {
            throw new NotImplementedException();
        }

        public Task<HealthCertificate> DeleteHealthCertificate(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HealthCertificate>> GetAllHealthCertificates()
        {
            throw new NotImplementedException();
        }

        public Task<HealthCertificate> GetHealthCertificateById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HealthCertificate> RestoreHealthCertificate(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HealthCertificate> UpdateHealthCertificate(int id, UpdateHealthCertificateDTO healthCertificate)
        {
            throw new NotImplementedException();
        }
    }
}
