using BusinessObjects.Enums;
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
            // Tạo mới một đối tượng KoiFishy
            var koi = new KoiFishy
            {
                CategoryId = createKoiFishy.CategoryId,
                Price = createKoiFishy.Price,
                Quantity = createKoiFishy.Quantity,
                Status = KoiFishStatus.Active.ToString(),
                CreatedDate = DateTime.Now,
            };

            // Thêm KoiFishy vào cơ sở dữ liệu
            await _koiFishyRepository.AddAsync(koi);

            // Upload danh sách ảnh và nhận về các URL
            List<string> imageUrls = await _imageService.UploadKoiFishyImage(createKoiFishy.Img, koi.Id);

            // Lưu thông tin các ảnh vào bảng Image
            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    KoiFishyId = koi.Id,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }

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
            // Lấy thông tin KoiFishy từ database
            var koi = await _koiFishyRepository.GetByIdAsync(id);
            if (koi == null)
            {
                throw new Exception($"Koi with ID {id} is not found");
            }

            // Cập nhật các thuộc tính cơ bản của KoiFishy
            koi.Price = updateKoiFishy.Price;
            koi.Quantity = updateKoiFishy.Quantity;
            koi.Status = updateKoiFishy.Status;
            koi.ModifiedDate = DateTime.Now;

            // Nếu có danh sách ảnh được upload trong yêu cầu cập nhật
            if (updateKoiFishy.Img != null && updateKoiFishy.Img.Any())
            {
                // Lấy danh sách ảnh hiện tại của KoiFishy từ database
                var existingImages = await _imageRepository.GetByKoiFishyIdAsync(koi.Id);

                // Xóa tất cả các ảnh cũ trên Cloudinary và trong cơ sở dữ liệu
                foreach (var existingImage in existingImages)
                {
                    // Xóa ảnh trên Cloudinary
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "KoiFishy");
                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }
                    // Xóa ảnh khỏi database
                    await _imageRepository.RemoveAsync(existingImage);
                }

                // Upload danh sách ảnh mới và lưu thông tin vào database
                List<string> newImageUrls = await _imageService.UploadKoiFishyImage(updateKoiFishy.Img, koi.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        KoiFishyId = koi.Id,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _imageRepository.AddAsync(newImage);
                }
            }

            // Cập nhật thông tin KoiFishy vào database
            await _koiFishyRepository.UpdateAsync(koi);
            return koi;
        }

    }
}
