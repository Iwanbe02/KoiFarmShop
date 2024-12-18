﻿using BusinessObjects.Enums;
using BusinessObjects.Helpers;
using BusinessObjects.Models;
using DataAccessObjects.DTOs.ConsignmentDTO;
using Microsoft.EntityFrameworkCore;
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
            var allKoiCodes = await _consignmentRepository.Entities()
                                                    .Select(c => c.KoiCode)
                                                    .ToListAsync();
            string newKoiCode = IdGenerator.GenerateId(allKoiCodes, "K");

            var consignment = new Consignment
            {
                AccountId = createConsignment.AccountId,
                KoiCode = newKoiCode,
                Name = createConsignment.Name,
                YearOfBirth = createConsignment.YearOfBirth,
                Gender = createConsignment.Gender,
                Origin = createConsignment.Origin,
                Variety = createConsignment.Variety,
                Character = createConsignment.Character,
                Size = createConsignment.Size,
                AmountFood = createConsignment.AmountFood,
                Status = OrderStatus.Pending.ToString(),
                CreatedDate = DateTime.Now
            };
            await _consignmentRepository.AddAsync(consignment);

            List<string> imageUrls = await _imageService.UploadConsignmentImage(createConsignment.Img, consignment.Id);

            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    ConsignmentId = consignment.Id,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }
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
            consignment.Name = updateConsignment.Name;
            consignment.YearOfBirth = updateConsignment.YearOfBirth;
            consignment.Gender = updateConsignment.Gender;
            consignment.Origin = updateConsignment.Origin;
            consignment.Variety = updateConsignment.Variety;
            consignment.Character = updateConsignment.Character;
            consignment.Size = updateConsignment.Size;
            consignment.AmountFood = updateConsignment.AmountFood;
            consignment.Price = updateConsignment.Price;
            consignment.Status = updateConsignment.Status;
            consignment.ModifiedDate = DateTime.Now;

            if (updateConsignment.Img != null && updateConsignment.Img.Any())
            {
                var existingImages = await _imageRepository.GetByConsignmentIdAsync(consignment.Id);

                foreach (var existingImage in existingImages)
                {
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "Consignment");
                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }
                    await _imageRepository.RemoveAsync(existingImage);
                }

                List<string> newImageUrls = await _imageService.UploadConsignmentImage(updateConsignment.Img, consignment.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        ConsignmentId = consignment.Id,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _imageRepository.AddAsync(newImage);
                }
            }
            else
            {
            }

            await _consignmentRepository.UpdateAsync(consignment);
            return consignment;
        }

        public async Task<Consignment> UpdateConsignmentStatus(int consignmentId, string newStatus)
        {
            var consignment = await _consignmentRepository.GetByIdAsync(consignmentId);
            if (consignment == null || consignment.IsDeleted == true)
            {
                throw new Exception("Consignment không tồn tại hoặc đã bị xóa.");
            }

            consignment.Status = newStatus;
            await _consignmentRepository.UpdateAsync(consignment);
            return consignment;
        }

        public async Task<Dictionary<int, Dictionary<string, decimal>>> GetMonthlyConsignments()
        {
            var consignment = await _consignmentRepository.GetAllAsync();

            var monthlySales = consignment
                .Where(consignment => consignment.Status == OrderStatus.Paid.ToString())
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

        public async Task<int> GetTotaConsignmentsByMonth(int month)
        {
            var consignment = await _consignmentRepository.GetAllAsync();

            var totalConsignments = consignment
                .Where(o => o.Status == OrderStatus.Paid.ToString() && o.CreatedDate.Month == month)
                .Count();
            return totalConsignments;
        }

        public async Task<decimal> GetTotalPriceConsignments()
        {
            var consignmentPrice = await _consignmentRepository.GetAllAsync();

            var totalPrice = consignmentPrice
                .Where(o => o.Status == OrderStatus.Paid.ToString())
                .Sum(o => o.Price);

            return totalPrice;
        }
    }
}
