using BusinessObjects.Models;
using DataAccessObjects.DTOs.CartDTO;
using DataAccessObjects.DTOs.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ICartService
    {
        Task<IEnumerable<Cart>> GetAllCarts();
        Task<Cart> GetCartById(int id);
        Task<Cart> CreateCart(CreateCartDTO createCart);
        Task<Cart> UpdateCart(int id, UpdateCartDTO updateCart);
        Task<Cart> DeleteCart(int id);
        Task<Cart> RestoreCart(int id);
    }
}
