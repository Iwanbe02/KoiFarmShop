using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetByOrderIdAsync(int orderId);
    }
}
