using BusinessObjects.Enums;
using BusinessObjects.Models;
using DataAccessObjects.DTOs.OrriginCetificateDTO;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class OriginCertificateService : IOriginCertificateService
    {
        private readonly IOriginCertificateRepository _originCertificateRepository;
        public OriginCertificateService(IOriginCertificateRepository originCertificateRepository)
        {
            _originCertificateRepository = originCertificateRepository;
        }
        public async Task<OriginCertificate> CreateOriginCertificate(CreateOriginCertificateDTO createOriginCertificate)
        {
            var originCertificate = new OriginCertificate
            {
                KoiId = createOriginCertificate.KoiId,
                Variety = createOriginCertificate.Variety,
                Gender = createOriginCertificate.Gender,
                Size = createOriginCertificate.Size,
                YearOfBirth = createOriginCertificate.YearOfBirth,
                Date = createOriginCertificate.Date,
                Signature = createOriginCertificate.Signature,
                Location = createOriginCertificate.Location,
                CreatedDate = DateTime.Now
            };
            await _originCertificateRepository.AddAsync(originCertificate);
            return originCertificate;
        }

        public async Task<OriginCertificate> DeleteOriginCertificate(int id)
        {
            var originCertificate = await _originCertificateRepository.GetByIdAsync(id);
            if(originCertificate == null)
            {
                throw new Exception($"Origin with ID{id} is not found");
            }
            originCertificate.IsDeleted = true;
            originCertificate.DeletedDate = DateTime.Now;
            await _originCertificateRepository.UpdateAsync(originCertificate);
            return originCertificate;
        }

        public async Task<IEnumerable<OriginCertificate>> GetAllOriginCertificate()
        {
            return await _originCertificateRepository.GetAllAsync();
        }

        public async Task<OriginCertificate> GetOriginCertificateById(int id)
        {
            return await _originCertificateRepository.GetByIdAsync(id);
        }

        public async Task<OriginCertificate> RestoreOriginCertificate(int id)
        {
            var originCertificate = await _originCertificateRepository.GetByIdAsync(id);
            if (originCertificate == null)
            {
                throw new Exception($"Origin with ID{id} is not found");
            }
            if (originCertificate.IsDeleted == true)
            {
                originCertificate.IsDeleted = false;
                await _originCertificateRepository.UpdateAsync(originCertificate);

            }
            return originCertificate;
        }

        public async Task<OriginCertificate> UpdateOriginCertificate(int id, UpdateOriginCertificateDTO updateOriginCertificate)
        {
            var originCertificate = await _originCertificateRepository.GetByIdAsync(id);
            if (originCertificate == null)
            {
                throw new Exception($"Origin with ID{id} is not found");
            }
            originCertificate.Variety = updateOriginCertificate.Variety;
            originCertificate.Gender = updateOriginCertificate.Gender;
            originCertificate.Size = updateOriginCertificate.Size;
            originCertificate.YearOfBirth = updateOriginCertificate.YearOfBirth;
            originCertificate.Date = updateOriginCertificate.Date;
            originCertificate.Signature = updateOriginCertificate.Signature;
            originCertificate.Location = updateOriginCertificate.Location;
            originCertificate.ModifiedDate = DateTime.Now;

            await _originCertificateRepository.UpdateAsync(originCertificate);
            return originCertificate;
        }
    }
}
