using BusinessObjects.Models;
using DataAccessObjects.DTOs.KoiFishDTO;
using Repositories.Implement;
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
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        public KoiFishService(IKoiFishRepository koiFishRepository, IImageService imageService, IImageRepository imageRepository)
        {
            _koiFishRepository = koiFishRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;
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
                Status = createKoiFish.Status,
            };
            await _koiFishRepository.AddAsync(koi);
            string url = await _imageService.UploadKoiImage(createKoiFish.Img, koi.Id);
            var image = new Image
            {
                UrlPath = url,
                KoiId = koi.Id,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };
            await _imageRepository.AddAsync(image);
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
            koi.Status = updateKoiFish.Status;

            await _koiFishRepository.UpdateAsync(koi);
            return koi;
        }
    }
}
