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
                CreatedDate = DateTime.Now,
            };
            await _koiFishyRepository.AddAsync(koi);
            string url = await _imageService.UploadKoiFishyImage(createKoiFishy.Img, koi.Id);
            var image = new Image
            {
                UrlPath = url,
                KoiFishyId = koi.Id,
                CreatedDate = DateTime.Now,
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
            koi.DeletedDate = DateTime.Now;
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
            koi.Price = updateKoiFishy.Price;
            koi.Quantity = updateKoiFishy.Quantity;
            koi.Status = updateKoiFishy.Status;
            koi.ModifiedDate = DateTime.Now;

            if (updateKoiFishy.Img != null)
            {
                // Lấy ảnh hiện tại liên kết với KoiFishy
                var existingImage = await _imageRepository.GetByKoiFishyIdAsync(koi.Id);

                if (existingImage != null)
                {
                    // Xóa ảnh cũ trên Cloudinary
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "KoiImages"); 

                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }

                    // Tải ảnh mới lên Cloudinary và lấy URL
                    string newImageUrl = await _imageService.UploadKoiFishyImage(updateKoiFishy.Img, koi.Id);

                    // Cập nhật URL của ảnh cũ
                    existingImage.UrlPath = newImageUrl;
                    existingImage.ModifiedDate = DateTime.Now;

                    // Lưu thay đổi vào database
                    await _imageRepository.UpdateAsync(existingImage);
                }
                else
                {
                    // Nếu không có ảnh cũ, tạo ảnh mới
                    string newImageUrl = await _imageService.UploadKoiFishyImage(updateKoiFishy.Img, koi.Id);

                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        KoiFishyId = koi.Id,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                    };

                    await _imageRepository.AddAsync(newImage);
                }
            }

            await _koiFishyRepository.UpdateAsync(koi);
            return koi;
        }
    }
}
