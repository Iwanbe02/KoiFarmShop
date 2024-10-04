using BusinessObjects.Models;
using DataAccessObjects.DTOs.KoiFishDTO;
using DataAccessObjects.DTOs.OrriginCetificateDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IOriginCertificateService
    {
        Task<IEnumerable<OriginCertificate>> GetAllOriginCertificate();
        Task<OriginCertificate> GetOriginCertificateById(int id);
        Task<OriginCertificate> CreateOriginCertificate(CreateOriginCertificateDTO createOriginCertificate);
        Task<OriginCertificate> UpdateOriginCertificate(int id, UpdateOriginCertificateDTO updateOriginCertificate);
        Task<OriginCertificate> DeleteOriginCertificate(int id);
        Task<OriginCertificate> RestoreOriginCertificate(int id);
    }
}
