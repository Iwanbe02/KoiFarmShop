using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        private readonly KoiFarmShopContext _dbContext;
        public CartItemRepository(KoiFarmShopContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<IEnumerable<CartItem>> GetByOrderIdAsync(int orderId)
        {
            // Lấy CartId từ Order
            var order = await _dbContext.Orders
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();

            if (order == null || !order.CartId.HasValue)
            {
                return Enumerable.Empty<CartItem>();  // Nếu không có CartId, trả về danh sách rỗng
            }

            // Dùng CartId để lấy CartItem
            var cartItems = await _dbContext.CartItems
                .Where(ci => ci.CartId == order.CartId.Value)
                .ToListAsync();

            return cartItems;
        }
    }
}
