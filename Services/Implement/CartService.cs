using BusinessObjects.Models;
using DataAccessObjects.DTOs.CartDTO;
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
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Cart> CreateCart(CreateCartDTO createCart)
        {
            var cart = new Cart
            {
                KoiId = createCart.KoiId,
                KoiFishyId = createCart.KoiFishyId,
                Price = createCart.Price,
                CreatedDate = DateTime.Now
            };
            await _cartRepository.AddAsync(cart);
            return cart;
        }

        public async Task<Cart> DeleteCart(int id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            if (cart == null)
            {
                throw new Exception($"Cart with ID{id} is not found");
            }
            cart.IsDeleted = true;
            cart.DeletedDate = DateTime.Now;
            await _cartRepository.UpdateAsync(cart);
            return cart;
        }

        public async Task<IEnumerable<Cart>> GetAllCarts()
        {
            return await _cartRepository.GetAllAsync();
        }

        public async Task<Cart> GetCartById(int id)
        {
            return await _cartRepository.GetByIdAsync(id);
        }

        public async Task<Cart> RestoreCart(int id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            if (cart == null)
            {
                throw new Exception($"Cart with ID{id} is not found");
            }
            if (cart.IsDeleted == true)
            {
                cart.IsDeleted = false;
                await _cartRepository.UpdateAsync(cart);
            }
            return cart;
        }

        public async Task<Cart> UpdateCart(int id, UpdateCartDTO updateCart)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            if (cart == null)
            {
                throw new Exception($"Cart with ID{id} is not found");
            }
            cart.Price = updateCart.Price;
            cart.ModifiedDate = DateTime.Now;
            await _cartRepository.UpdateAsync(cart);
            return cart;
        }
    }
}
