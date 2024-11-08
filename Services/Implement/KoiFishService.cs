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
                CreatedDate = DateTime.Now
            };
            await _koiFishRepository.AddAsync(koi);
            string url = await _imageService.UploadKoiImage(createKoiFish.Img, koi.Id);
            var image = new Image
            {
                UrlPath = url,
                KoiId = koi.Id,
                CreatedDate = DateTime.Now,
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
            koi.ModifiedDate = DateTime.Now;

            if (updateKoiFish.Img != null)
            {
                // Lấy ảnh hiện tại liên kết với KoiFishy
                var existingImage = await _imageRepository.GetByKoiIdAsync(koi.Id);

                if (existingImage != null)
                {
                    // Xóa ảnh cũ trên Cloudinary
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "KoiImages");

                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }

                    // Tải ảnh mới lên Cloudinary và lấy URL
                    string newImageUrl = await _imageService.UploadKoiImage(updateKoiFish.Img, koi.Id);

                    // Cập nhật URL của ảnh cũ
                    existingImage.UrlPath = newImageUrl;
                    existingImage.ModifiedDate = DateTime.Now;

                    // Lưu thay đổi vào database
                    await _imageRepository.UpdateAsync(existingImage);
                }
                else
                {
                    // Nếu không có ảnh cũ, tạo ảnh mới
                    string newImageUrl = await _imageService.UploadKoiImage(updateKoiFish.Img, koi.Id);

                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        KoiId = koi.Id,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                    };

                    await _imageRepository.AddAsync(newImage);
                }
            }

            await _koiFishRepository.UpdateAsync(koi);
            return koi;
        }
    }
}
