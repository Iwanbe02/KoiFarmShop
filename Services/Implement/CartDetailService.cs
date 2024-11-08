using BusinessObjects.Models;
using DataAccessObjects.DTOs.CartDetailDTO;
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
    public class CartDetailService : ICartDetailService
    {
        private readonly ICartDetailRepository _cartDetailRepository;
        public CartDetailService(ICartDetailRepository cartDetailRepository)
        {
            _cartDetailRepository = cartDetailRepository;
        }
        public async Task<CartDetail> CreateCartDetail(CreateCartDetailDTO createCartDetail)
        {
            var cartDetail = new CartDetail
            {
                KoiId = createCartDetail.KoiId,
                CartId = createCartDetail.CartId,
                FeedbackId = createCartDetail.FeedbackId,
                Price = createCartDetail.Price,
                Status = createCartDetail.Status,
                CreatedDate = DateTime.Now
            };
            await _cartDetailRepository.AddAsync(cartDetail);
            return cartDetail;
        }

        public async Task<CartDetail> DeleteCartDetail(int id)
        {
            var cartDetail = await _cartDetailRepository.GetByIdAsync(id);
            if (cartDetail == null)
            {
                throw new Exception($"Cart detail with ID{id} is not found");
            }
            cartDetail.IsDeleted = true;
            cartDetail.DeletedDate = DateTime.Now;
            await _cartDetailRepository.UpdateAsync(cartDetail);
            return cartDetail;
        }

        public async Task<IEnumerable<CartDetail>> GetAllCartDetails()
        {
            return await _cartDetailRepository.GetAllAsync();
        }

        public async Task<CartDetail> GetCartDetailById(int id)
        {
            return await _cartDetailRepository.GetByIdAsync(id);
        }

        public async Task<CartDetail> RestoreCartDetail(int id)
        {
            var cartDetail = await _cartDetailRepository.GetByIdAsync(id);
            if (cartDetail == null)
            {
                throw new Exception($"Cart detail with ID{id} is not found");
            }
            if (cartDetail.IsDeleted == true)
            {
                cartDetail.IsDeleted = false;
                await _cartDetailRepository.UpdateAsync(cartDetail);
            }
            return cartDetail;
        }

        public async Task<CartDetail> UpdateCartDetail(int id, UpdateCartDetailDTO updateCartDetail)
        {
            var cartDetail = await _cartDetailRepository.GetByIdAsync(id);
            if (cartDetail == null)
            {
                throw new Exception($"Cart detail with ID{id} is not found");
            }
            cartDetail.Price = updateCartDetail.Price;
            cartDetail.Status = updateCartDetail.Status;
            cartDetail.ModifiedDate = DateTime.Now;

            await _cartDetailRepository.UpdateAsync(cartDetail);
            return cartDetail;
        }
    }
}
