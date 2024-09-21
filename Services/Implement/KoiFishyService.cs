using BusinessObjects.Models;
using DataAccessObjects.DTOs.KoiFishyDTO;
using Repositories.Implement;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class KoiFishyService : IKoiFishyService
    {
        private readonly IKoiFishyRepository _koiFishyRepository;
        public KoiFishyService(IKoiFishyRepository koiFishyRepository)
        {
            _koiFishyRepository = koiFishyRepository;
        }
        public async Task<KoiFishy> CreateKoiFishy(CreateKoiFishyDTO createKoiFishy)
        {
            var koi = new KoiFishy
            {
                CategoryId = createKoiFishy.CategoryId,
                Quantity = createKoiFishy.Quantity,
                Status = createKoiFishy.Status,
            };
            await _koiFishyRepository.AddAsync(koi);
            return koi;
        }

        public async Task<KoiFishy> DeleteKoiFishy(int id)
        {
            var koi = await _koiFishyRepository.GetByIdAsync(id);
            if (koi == null)
            {
                throw new Exception($"Koi with ID{id} is not found");
            }
            koi.IsDeleted = true;
            await _koiFishyRepository.UpdateAsync(koi);
            return koi;
        }

        public async Task<IEnumerable<KoiFishy>> GetAllKoiFishes()
        {
            return await _koiFishyRepository.GetAllAsync();
        }

        public async Task<KoiFishy> GetKoiFishyById(int id)
        {
            return await _koiFishyRepository.GetByIdAsync(id);
        }

        public async Task<KoiFishy> RestoreKoiFishy(int id)
        {
            var koi = await _koiFishyRepository.GetByIdAsync(id);
            if (koi == null)
            {
                throw new Exception($"Koi with ID{id} is not found");
            }
            if (koi.IsDeleted == true)
            {
                koi.IsDeleted = false;
                await _koiFishyRepository.UpdateAsync(koi);
            }
            return koi;
        }

        public async Task<KoiFishy> UpdateKoiFishy(int id, UpdateKoiFishyDTO updateKoiFishy)
        {
            var koi = await _koiFishyRepository.GetByIdAsync(id);
            if (koi == null)
            {
                throw new Exception($"Koi with ID{id} is not found");
            }
            koi.CategoryId = updateKoiFishy.CategoryId;
            koi.Quantity = updateKoiFishy.Quantity;
            koi.Status = updateKoiFishy.Status;

            await _koiFishyRepository.UpdateAsync(koi);
            return koi;
        }
    }
}
