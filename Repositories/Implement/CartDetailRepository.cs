using BusinessObjects.Models;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class CartDetailRepository : GenericRepository<CartDetail>, ICartDetailRepository
    {
        public CartDetailRepository(KoiFarmShopContext context) : base(context)
        {
        }
    }
}
