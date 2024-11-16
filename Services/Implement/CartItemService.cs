﻿using BusinessObjects.Models;
using DataAccessObjects.DTOs.CartItemDTO;
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
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        public CartItemService(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }
        public async Task<CartItem> CreateCartItem(CreateCartItemDTO createCartItem)
        {
            var cart = new CartItem
            {
                CartId = createCartItem.CartId,
                KoiFishId = createCartItem.KoiFishId,
                KoiFishyId = createCartItem.KoiFishyId,
                ConsignmentId = createCartItem.ConsignmentId,
                Price = createCartItem.Price,
                CreatedDate = DateTime.Now
            };
            await _cartItemRepository.AddAsync(cart);
            return cart;
        }

        public async Task<CartItem> DeleteCartItem(int id)
        {
            var cart = await _cartItemRepository.GetByIdAsync(id);
            if (cart == null)
            {
                throw new Exception($"Cart item with ID{id} is not found");
            }
            cart.IsDeleted = true;
            cart.DeletedDate = DateTime.Now;
            await _cartItemRepository.UpdateAsync(cart);
            return cart;
        }

        public async Task<IEnumerable<CartItem>> GetAllCartItems()
        {
            return await _cartItemRepository.GetAllAsync();
        }

        public async Task<CartItem> GetCartItemById(int id)
        {
            return await _cartItemRepository.GetByIdAsync(id);
        }

        public async Task<CartItem> RestoreCartItem(int id)
        {
            var cart = await _cartItemRepository.GetByIdAsync(id);
            if (cart == null)
            {
                throw new Exception($"Cart item with ID{id} is not found");
            }
            if (cart.IsDeleted == true)
            {
                cart.IsDeleted = false;
                await _cartItemRepository.UpdateAsync(cart);
            }
            return cart;
        }

        public async Task<CartItem> UpdateCartItem(int id, UpdateCartItemDTO updateCartItem)
        {
            var cart = await _cartItemRepository.GetByIdAsync(id);
            if (cart == null)
            {
                throw new Exception($"Cart item with ID{id} is not found");
            }
            cart.Price = updateCartItem.Price;
            cart.ModifiedDate = DateTime.Now;
            await _cartItemRepository.UpdateAsync(cart);
            return cart;
        }
    }
}