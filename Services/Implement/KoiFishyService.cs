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
                Name = createKoiFishy.Name,
                Gender = createKoiFishy.Gender,
                Size = createKoiFishy.Size,
                YearOfBirth = createKoiFishy.YearOfBirth,
                Variety = createKoiFishy.Variety,
                Origin = createKoiFishy.Origin,
                Diet = createKoiFishy.Diet,
                Character = createKoiFishy.Character,
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
            koi.Status = KoiFishStatus.InActive.ToString();
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
            koi.Name = updateKoiFishy.Name;
            koi.Gender = updateKoiFishy.Gender;
            koi.Size = updateKoiFishy.Size;
            koi.YearOfBirth = updateKoiFishy.YearOfBirth;
            koi.Variety = updateKoiFishy.Variety;
            koi.Origin = updateKoiFishy.Origin;
            koi.Diet = updateKoiFishy.Diet;
            koi.Character = updateKoiFishy.Character;
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
        public async Task<KoiFishy> UpdateKoiFishyStatus(int koiFishyId, string newStatus)
        {
            var koiFishy = await _koiFishyRepository.GetByIdAsync(koiFishyId);
            if (koiFishy == null || koiFishy.IsDeleted == true)
            {
                throw new Exception("KoiFishy không tồn tại hoặc đã bị xóa.");
            }

            koiFishy.Status = newStatus;
            await _koiFishyRepository.UpdateAsync(koiFishy);
            return koiFishy;
        }

        public async Task<Dictionary<int, Dictionary<string, decimal>>> GetMonthlyKoiFishy()
        {
            var koi = await _koiFishyRepository.GetAllAsync();

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

        public async Task<int> GetTotalKoiFishyByMonth(int month)
        {
            var koi = await _koiFishyRepository.GetAllAsync();

            var totalKoi = koi
                .Where(o => o.Status == KoiFishStatus.Sold.ToString() && o.CreatedDate.Month == month)
                .Count();
            return totalKoi;
        }

        public async Task<decimal> GetTotalPriceKoiFishy()
        {
            var koiPrice = await _koiFishyRepository.GetAllAsync();

            var totalPrice = koiPrice
                .Where(o => o.Status == KoiFishStatus.Sold.ToString())
                .Sum(o => o.Price);

            return totalPrice;
        }
    }
}
