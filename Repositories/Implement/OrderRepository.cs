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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly KoiFarmShopContext _context;
        public OrderRepository(KoiFarmShopContext context) : base(context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Order>> GetAllWithIncludesAsync()
        {
            return await _context.Orders
               .Include(o => o.Cart)
                    .ThenInclude(c => c.CartItems)
                    .ThenInclude(ci => ci.KoiFish)
                .Include(o => o.Cart)
                    .ThenInclude(c => c.CartItems)
                    .ThenInclude(ci => ci.KoiFishy)
                .Include(o => o.Cart)
                    .ThenInclude(c => c.CartItems)
                    .ThenInclude(ci => ci.Consignment)
                .ToListAsync();
        }

    }
}
