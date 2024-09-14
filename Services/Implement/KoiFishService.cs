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
                Amount = createKoiFish.Amount,
                Type = createKoiFish.Type,
                Date = createKoiFish.Date,
                IsDelete = createKoiFish.IsDelete,
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
            updateKoiFish.CategoryId = koi.CategoryId;
            updateKoiFish.Origin = koi.Origin;
            updateKoiFish.Gender = koi.Gender;
            updateKoiFish.Age = koi.Age;
            updateKoiFish.Size = koi.Size;
            updateKoiFish.Species = koi.Species;
            updateKoiFish.Character = koi.Character;
            updateKoiFish.AmountFood = koi.AmountFood;
            updateKoiFish.ScreeningRate = koi.ScreeningRate;
            updateKoiFish.Amount = koi.Amount;
            updateKoiFish.Type = koi.Type;
            updateKoiFish.Date = koi.Date;
            updateKoiFish.IsDelete = koi.IsDelete;

            await _koiFishRepository.UpdateAsync(koi);
            return koi;
        }
    }
}
