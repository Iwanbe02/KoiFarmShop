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
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        public KoiFishyService(IKoiFishyRepository koiFishyRepository, IImageService imageService, IImageRepository imageRepository)
        {
            _koiFishyRepository = koiFishyRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;
        }
        public async Task<KoiFishy> CreateKoiFishy(CreateKoiFishyDTO createKoiFishy)
        {
            var koi = new KoiFishy
            {
                CategoryId = createKoiFishy.CategoryId,
                Price = createKoiFishy.Price,
                Quantity = createKoiFishy.Quantity,
                Status = createKoiFishy.Status,
            };
            await _koiFishyRepository.AddAsync(koi);
            string url = await _imageService.UploadKoiFishyImage(createKoiFishy.Img, koi.Id);
            var image = new Image
            {
                UrlPath = url,
                KoiFishyId = koi.Id,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };
            await _imageRepository.AddAsync(image);
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
            koi.Price = updateKoiFishy.Price;
            koi.Quantity = updateKoiFishy.Quantity;
            koi.Status = updateKoiFishy.Status;

            await _koiFishyRepository.UpdateAsync(koi);
            return koi;
        }
    }
}
