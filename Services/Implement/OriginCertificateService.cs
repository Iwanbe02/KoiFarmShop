using BusinessObjects.Enums;
using BusinessObjects.Models;
using DataAccessObjects.DTOs.OrriginCetificateDTO;
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
    public class OriginCertificateService : IOriginCertificateService
    {
        private readonly IOriginCertificateRepository _originCertificateRepository;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        public OriginCertificateService(IOriginCertificateRepository originCertificateRepository, IImageRepository imageRepository, IImageService imageService)
        {
            _originCertificateRepository = originCertificateRepository;
            _imageRepository = imageRepository;
            _imageService = imageService;
        }
        public async Task<OriginCertificate> CreateOriginCertificate(CreateOriginCertificateDTO createOriginCertificate)
        {
            var originCertificate = new OriginCertificate
            {
                Variety = createOriginCertificate.Variety,
                Gender = createOriginCertificate.Gender,
                Size = createOriginCertificate.Size,
                YearOfBirth = createOriginCertificate.YearOfBirth,
                Date = createOriginCertificate.Date,
                PlaceOfIssue = createOriginCertificate.PlaceOfIssue,
                CreatedDate = DateTime.Now
            };
            await _originCertificateRepository.AddAsync(originCertificate);

            string imageUrl = await _imageService.UploadCertificateImage(createOriginCertificate.Img, originCertificate.Id);
            var image = new Image
            {
                UrlPath = imageUrl,  
                OriginCertificateId = originCertificate.Id,  
                CreatedDate = DateTime.Now,  
                IsDeleted = false,  
            };

            await _imageRepository.AddAsync(image);;  
            return originCertificate;
        }

        public async Task<OriginCertificate> DeleteOriginCertificate(int id)
        {
            var originCertificate = await _originCertificateRepository.GetByIdAsync(id);
            if(originCertificate == null)
            {
                throw new Exception($"Origin with ID{id} is not found");
            }
            originCertificate.IsDeleted = true;
            originCertificate.DeletedDate = DateTime.Now;
            await _originCertificateRepository.UpdateAsync(originCertificate);
            return originCertificate;
        }

        public async Task<IEnumerable<OriginCertificate>> GetAllOriginCertificate()
        {
            return await _originCertificateRepository.GetAllAsync();
        }

        public async Task<OriginCertificate> GetOriginCertificateById(int id)
        {
            return await _originCertificateRepository.GetByIdAsync(id);
        }

        public async Task<OriginCertificate> RestoreOriginCertificate(int id)
        {
            var originCertificate = await _originCertificateRepository.GetByIdAsync(id);
            if (originCertificate == null)
            {
                throw new Exception($"Origin with ID{id} is not found");
            }
            if (originCertificate.IsDeleted == true)
            {
                originCertificate.IsDeleted = false;
                await _originCertificateRepository.UpdateAsync(originCertificate);

            }
            return originCertificate;
        }

        public async Task<OriginCertificate> UpdateOriginCertificate(int id, UpdateOriginCertificateDTO updateOriginCertificate)
        {
            var originCertificate = await _originCertificateRepository.GetByIdAsync(id);
            if (originCertificate == null)
            {
                throw new Exception($"Origin with ID{id} is not found");
            }
            originCertificate.Variety = updateOriginCertificate.Variety;
            originCertificate.Gender = updateOriginCertificate.Gender;
            originCertificate.Size = updateOriginCertificate.Size;
            originCertificate.YearOfBirth = updateOriginCertificate.YearOfBirth;
            originCertificate.Date = updateOriginCertificate.Date;
            originCertificate.PlaceOfIssue = updateOriginCertificate.PlaceOfIssue;
            originCertificate.ModifiedDate = DateTime.Now;

            await _originCertificateRepository.UpdateAsync(originCertificate);

            if (updateOriginCertificate.Img != null)
            {
                var existingImage = await _imageRepository.GetByCertificateIdAsync(originCertificate.Id);

                if (existingImage != null)
                {
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "Certificate");

                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }
                    await _imageRepository.RemoveAsync(existingImage);

                    string newImageUrl = await _imageService.UploadCertificateImage(updateOriginCertificate.Img, originCertificate.Id);
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        OriginCertificateId = originCertificate.Id,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                    };

                    await _imageRepository.AddAsync(newImage);
                }
            }
            else
            {
            }

            await _originCertificateRepository.UpdateAsync(originCertificate);
            return originCertificate;
        }
    }
}
