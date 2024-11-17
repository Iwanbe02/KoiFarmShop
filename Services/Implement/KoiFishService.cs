using BusinessObjects.Enums;
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
                OriginCertificateId = createKoiFish.OriginCertificateId,
                CategoryId = createKoiFish.CategoryId,
                Name = createKoiFish.Name,
                Price = createKoiFish.Price,
                Origin = createKoiFish.Origin,
                Gender = createKoiFish.Gender,
                YearOfBirth = createKoiFish.YearOfBirth,
                Size = createKoiFish.Size,
                Variety = createKoiFish.Variety,
                Character = createKoiFish.Character,
                Diet = createKoiFish.Diet,
                AmountFood = createKoiFish.AmountFood,
                ScreeningRate = createKoiFish.ScreeningRate,
                Type = createKoiFish.Type,
                Status = KoiFishStatus.Active.ToString(),
                CreatedDate = DateTime.Now
            };
            await _koiFishRepository.AddAsync(koi);

            List<string> imageUrls = await _imageService.UploadKoiImage(createKoiFish.Img, koi.Id);
            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    KoiId = koi.Id,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }
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
            koi.DeletedDate = DateTime.Now;
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
            koi.Name = updateKoiFish.Name;
            koi.Price = updateKoiFish.Price;
            koi.Origin = updateKoiFish.Origin;
            koi.Gender = updateKoiFish.Gender;
            koi.YearOfBirth = updateKoiFish.YearOfBirth;
            koi.Size = updateKoiFish.Size;
            koi.Variety = updateKoiFish.Variety;
            koi.Character = updateKoiFish.Character;
            koi.AmountFood = updateKoiFish.AmountFood;
            koi.Diet = updateKoiFish.Diet;
            koi.ScreeningRate = updateKoiFish.ScreeningRate;
            koi.Type = updateKoiFish.Type;
            koi.Status = updateKoiFish.Status;
            koi.ModifiedDate = DateTime.Now;

            if (updateKoiFish.Img != null && updateKoiFish.Img.Any())
            {
                var existingImages = await _imageRepository.GetByKoiIdAsync(koi.Id);

                foreach (var existingImage in existingImages)
                {
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "Koi");
                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }

                    await _imageRepository.RemoveAsync(existingImage);
                }

                List<string> newImageUrls = await _imageService.UploadKoiImage(updateKoiFish.Img, koi.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        KoiId = koi.Id,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _imageRepository.AddAsync(newImage);
                }
            }

            await _koiFishRepository.UpdateAsync(koi);
            return koi;
        }

        public async Task<KoiFish> UpdateKoiFishStatus(int koiFishId, string newStatus)
        {
            var koiFish = await _koiFishRepository.GetByIdAsync(koiFishId);
            if (koiFish == null || koiFish.IsDeleted == true)
            {
                throw new Exception("KoiFish không tồn tại hoặc đã bị xóa.");
            }

            koiFish.Status = newStatus;
            await _koiFishRepository.UpdateAsync(koiFish);
            return koiFish;
        }

        public async Task<Dictionary<int, Dictionary<string, decimal>>> GetMonthlyKoiFish()
        {
            var koi = await _koiFishRepository.GetAllAsync();

            var monthlySales = koi
                .Where(koi => koi.Status == KoiFishStatus.Sold.ToString())
                .GroupBy(d => new { d.CreatedDate.Year, d.CreatedDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalPrice = g.Sum(d => d.Price)
                });

            var result = new Dictionary<int, Dictionary<string, decimal>>();

            foreach (var item in monthlySales)
            {
                if (!result.ContainsKey(item.Year))
                {
                    result[item.Year] = new Dictionary<string, decimal>();
                }

                string monthName = new DateTime(item.Year, item.Month, 1).ToString("MMMM");

                result[item.Year][monthName] = item.TotalPrice;
            }

            foreach (var year in result.Keys)
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthName = new DateTime(year, month, 1).ToString("MMMM");
                    if (!result[year].ContainsKey(monthName))
                    {
                        result[year][monthName] = 0m;
                    }
                }
            }

            return result;
        }

        public async Task<int> GetTotalKoiFishByMonth(int month)
        {
            var koi = await _koiFishRepository.GetAllAsync();

            var totalKoi = koi
                .Where(o => o.Status == KoiFishStatus.Sold.ToString() && o.CreatedDate.Month == month)
                .Count();
            return totalKoi;
        }

        public async Task<decimal> GetTotalPriceKoiFish()
        {
            var koiPrice = await _koiFishRepository.GetAllAsync();

            var totalPrice = koiPrice
                .Where(o => o.Status == KoiFishStatus.Sold.ToString())
                .Sum(o => o.Price);

            return totalPrice;
        }
    }
}
