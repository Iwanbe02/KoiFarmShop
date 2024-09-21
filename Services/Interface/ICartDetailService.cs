using BusinessObjects.Models;
using DataAccessObjects.DTOs.CartDetailDTO;
using DataAccessObjects.DTOs.CartDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ICartDetailService
    {
        Task<IEnumerable<CartDetail>> GetAllCartDetails();
        Task<CartDetail> GetCartDetailById(int id);
        Task<CartDetail> CreateCartDetail(CreateCartDetailDTO createCartDetail);
        Task<CartDetail> UpdateCartDetail(int id, UpdateCartDetailDTO updateCartDetail);
        Task<CartDetail> DeleteCartDetail(int id);
        Task<CartDetail> RestoreCartDetail(int id);
    }
}
