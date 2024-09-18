using BusinessObjects.Models;
using DataAccessObjects.DTOs.KoiFishDTO;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class KoiFishService : IKoiFishService
    {
        private readonly IKoiFishRepository _koiFishRepository;
        public KoiFishService(IKoiFishRepository koiFishRepository)
        {
            _koiFishRepository = koiFishRepository;
        }
        public async Task<KoiFish> CreateKoiFish(CreateKoiFishDTO createKoiFish)
        {
            var koi = new KoiFish
            {
                CategoryId = createKoiFish.CategoryId,
                Origin = createKoiFish.Origin,
                Gender = createKoiFish.Gender,
                Age = createKoiFish.Age,
                Size = createKoiFish.Size,
                Species = createKoiFish.Species,
                Character = createKoiFish.Character,
                AmountFood = createKoiFish.AmountFood,
                ScreeningRate = createKoiFish.ScreeningRate,
                Type = createKoiFish.Type,
                Date = createKoiFish.Date,
                Status = createKoiFish.Status,
            };
            await _koiFishRepository.AddAsync(koi);
            return koi;
        }

        public async Task<KoiFish> DeleteKoiFish(int id)
        {
            var koi = await _koiFishRepository.GetByIdAsync(id);
            if (koi == null)
            {
                throw new Exception($"Koi with ID{id} is not found");
            }
            await _koiFishRepository.RemoveAsync(koi);
            return koi;
        }

        public async Task DeleteOrEnable(int koiId, bool isDeleted)
        {
            var koi = await _koiFishRepository.GetAsync(x => x.Id == koiId);
            if (koi == null)
            {
                throw new Exception($"Koi with ID{koiId} is not found");
            }
            koi.IsDeleted = isDeleted;
            await _koiFishRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<KoiFish>> GetAllKoiFishes()
        {
            return await _koiFishRepository.GetAllAsync();
        }

        public async Task<KoiFish> GetKoiFishById(int id)
        {
            return await _koiFishRepository.GetByIdAsync(id);
        }

        public async Task<KoiFish> UpdateKoiFish(int id, UpdateKoiFishDTO updateKoiFish)
        {
            var koi = await _koiFishRepository.GetByIdAsync(id);
            if (koi == null)
            {
                throw new Exception($"Koi with ID{id} is not found");
            }
            koi.CategoryId = updateKoiFish.CategoryId;
            koi.Origin = updateKoiFish.Origin;
            koi.Gender = updateKoiFish.Gender;
            koi.Age = updateKoiFish.Age;
            koi.Size = updateKoiFish.Size;
            koi.Species = updateKoiFish.Species;
            koi.Character = updateKoiFish.Character;
            koi.AmountFood = updateKoiFish.AmountFood;
            koi.ScreeningRate = updateKoiFish.ScreeningRate;
            koi.Type = updateKoiFish.Type;
            koi.Date = updateKoiFish.Date;
            koi.Status = updateKoiFish.Status;

            await _koiFishRepository.UpdateAsync(koi);
            return koi;
        }
    }
}
