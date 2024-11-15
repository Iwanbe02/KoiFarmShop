using BusinessObjects.Models;
using DataAccessObjects.DTOs.CartDTO;
using DataAccessObjects.DTOs.CartItemDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItem>> GetAllCartItems();
        Task<CartItem> GetCartItemById(int id);
        Task<CartItem> CreateCartItem(CreateCartItemDTO createCartItem);
        Task<CartItem> UpdateCartItem(int id, UpdateCartItemDTO updateCartItem);
        Task<CartItem> DeleteCartItem(int id);
        Task<CartItem> RestoreCartItem(int id);
    }
}
