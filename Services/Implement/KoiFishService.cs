using BusinessObjects.Models;
using DataAccessObjects.DTOs.KoiFishDTO;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

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
                Price = createKoiFish.Price,
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
            koi.IsDeleted = true;
            await _koiFishRepository.UpdateAsync(koi);
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

        public async Task<KoiFish> RestoreKoiFish(int id)
        {
            var koi = await _koiFishRepository.GetByIdAsync(id);
            if (koi == null)
            {
                throw new Exception($"Koi with ID{id} is not found");
            }
            if (koi.IsDeleted == true)
            {
                koi.IsDeleted = false;
                await _koiFishRepository.UpdateAsync(koi);
            }
            return koi;
        }

        public async Task<KoiFish> UpdateKoiFish(int id, UpdateKoiFishDTO updateKoiFish)
        {
            var koi = await _koiFishRepository.GetByIdAsync(id);
            if (koi == null)
            {
                throw new Exception($"Koi with ID{id} is not found");
            }
            koi.CategoryId = updateKoiFish.CategoryId;
            koi.Price = updateKoiFish.Price;
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
