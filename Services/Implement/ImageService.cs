using BusinessObjects.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Implement
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private Cloudinary _cloudinary;
        private string _cloudName;
        private string _apiKey;
        private string _apiSecret;
        private readonly IConfiguration _configuration;
        public ImageService(IImageRepository imageRepository, Cloudinary cloudinary, IConfiguration configuration)
        {
            _imageRepository = imageRepository;
            _cloudinary = cloudinary;
            _configuration = configuration;
            _cloudName = _configuration["Cloudinary:CloudName"]!;
            _apiKey = _configuration["Cloudinary:ApiKey"]!;
            _apiSecret = _configuration["Cloudinary:ApiSecret"]!;
        }
        public async Task<string> UploadKoiImage(IFormFile file, int KoiId)
        {
            if (!file.ContentType.ToLower().StartsWith("image/"))
            {
                throw new Exception("File is not a image!");
            }
            var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
            _cloudinary = new Cloudinary(account);
            var uploadParameters = new ImageUploadParams
            {
                // Tạo một folder riêng dựa trên KoiId
                Folder = $"KoiImages/{KoiId}", // Ví dụ: Folder theo KoiId
                PublicId = $"{KoiId}_{Path.GetFileNameWithoutExtension(file.FileName)}" // Tạo PublicId
            };
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                uploadParameters.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
            }
            var result = await _cloudinary.UploadAsync(uploadParameters);
            if (result.Error != null)
            {
                throw new Exception($"Error upload image: {result.Error.Message}");
            }
            return result.SecureUrl.ToString();
        }
        public async Task<string> UploadKoiFishyImage(IFormFile file, int KoiFishyId)
        {
            if (!file.ContentType.ToLower().StartsWith("image/"))
            {
                throw new Exception("File is not a image!");
            }
            var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
            _cloudinary = new Cloudinary(account);
            var uploadParameters = new ImageUploadParams
            {
                // Tạo một folder riêng dựa trên KoiId
                Folder = $"KoiImages/{KoiFishyId}",
                PublicId = $"{KoiFishyId}_{Path.GetFileNameWithoutExtension(file.FileName)}"
            };
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                uploadParameters.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
            }
            var result = await _cloudinary.UploadAsync(uploadParameters);
            if (result.Error != null)
            {
                throw new Exception($"Error upload image: {result.Error.Message}");
            }
            return result.SecureUrl.ToString();
        }

        public async Task<IEnumerable<Image>> GetAllImages()
        {
            return await _imageRepository.GetAllAsync();
        }
    }
}