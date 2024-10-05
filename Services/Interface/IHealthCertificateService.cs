using BusinessObjects.Models;
using DataAccessObjects.DTOs.HealthCertificateDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IHealthCertificateService
    {
        Task<IEnumerable<HealthCertificate>> GetAllHealthCertificates();
        Task<HealthCertificate> GetHealthCertificateById(int id);
        Task<HealthCertificate> CreateHealthCertificate(CreateHealthCertificateDTO createHealthCertificate);
        Task<HealthCertificate> UpdateHealthCertificate(int id, UpdateHealthCertificateDTO updateHealthCertificate);
        Task<HealthCertificate> DeleteHealthCertificate(int id);
        Task<HealthCertificate> RestoreHealthCertificate(int id);
    }
}
