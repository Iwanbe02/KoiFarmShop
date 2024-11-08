using BusinessObjects.Models;
using DataAccessObjects.DTOs.ConsignmentDTO;
using Microsoft.Identity.Client;
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
    public class ConsignmentService : IConsignmentService
    {
        private readonly IConsignmentRepository _consignmentRepository;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;

        public ConsignmentService(IConsignmentRepository consignmentRepository, IImageService imageService, IImageRepository imageRepository)
        {
            _consignmentRepository = consignmentRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;
        }
        public async Task<Consignment> CreateConsignment(CreateConsignmentDTO createConsignment)
        {
            var consignment = new Consignment
            {
                AccountId = createConsignment.AccountId,
                KoiId = createConsignment.KoiId,
                PaymentId = createConsignment.PaymentId,
                Price = createConsignment.Price,
                Status = createConsignment.Status,
                CreatedDate = DateTime.Now
            };
            await _consignmentRepository.AddAsync(consignment);

            string url = await _imageService.UploadConsignmentImage(createConsignment.Img, consignment.Id);
            var image = new Image
            {
                UrlPath = url,
                ConsignmentId = consignment.Id,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };
            await _imageRepository.AddAsync(image);
            return consignment;
        }

        public async Task<Consignment> DeleteConsignment(int id)
        {
            var consignment = await _consignmentRepository.GetByIdAsync(id);
            if (consignment == null)
            {
                throw new Exception($"Cart with ID{id} is not found");
            }
            consignment.IsDeleted = true;
            consignment.DeletedDate = DateTime.Now;
            await _consignmentRepository.UpdateAsync(consignment); 
            return consignment;
        }

        public async Task<IEnumerable<Consignment>> GetAllConsignments()
        {
            return await _consignmentRepository.GetAllAsync();
        }

        public async Task<Consignment> GetConsignmentById(int id)
        {
            return await _consignmentRepository.GetByIdAsync(id);
        }

        public async Task<Consignment> RestoreConsignment(int id)
        {
            var consignment = await _consignmentRepository.GetByIdAsync(id);
            if (consignment == null)
            {
                throw new Exception($"Cart with ID{id} is not found");
            }
            if (consignment.IsDeleted == true)
            {
                consignment.IsDeleted = false;
                await _consignmentRepository.UpdateAsync(consignment);
            }
            return consignment;
        }

        public async Task<Consignment> UpdateConsignment(int id, UpdateConsignmentDTO updateConsignment)
        {
            var consignment = await _consignmentRepository.GetByIdAsync(id);
            if (consignment == null)
            {
                throw new Exception($"Cart with ID{id} is not found");
            }
            consignment.Price = updateConsignment.Price;
            consignment.Status = updateConsignment.Status;
            consignment.ModifiedDate = DateTime.Now;

            if (updateConsignment.Img != null)
            {
                // Lấy ảnh hiện tại 
                var existingImage = await _imageRepository.GetByConsignmentIdAsync(consignment.Id);

                if (existingImage != null)
                {
                    // Xóa ảnh cũ trên Cloudinary
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "KoiImages");

                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }

                    // Tải ảnh mới lên Cloudinary và lấy URL
                    string newImageUrl = await _imageService.UploadConsignmentImage(updateConsignment.Img, consignment.Id);

                    // Cập nhật URL của ảnh cũ
                    existingImage.UrlPath = newImageUrl;
                    existingImage.ModifiedDate = DateTime.Now;

                    // Lưu thay đổi vào database
                    await _imageRepository.UpdateAsync(existingImage);
                }
                else
                {
                    // Nếu không có ảnh cũ, tạo ảnh mới
                    string newImageUrl = await _imageService.UploadConsignmentImage(updateConsignment.Img, consignment.Id);

                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        ConsignmentId = consignment.Id,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                    };

                    await _imageRepository.AddAsync(newImage);
                }
            }

            await _consignmentRepository.UpdateAsync(consignment);
            return consignment;
        }
    }
}
