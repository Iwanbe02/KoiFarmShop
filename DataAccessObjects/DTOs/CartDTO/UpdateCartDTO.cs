using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.CartDTO
{
    public class UpdateCartDTO
    {
        public int? Quantity { get; set; }

        public decimal? Price { get; set; }
    }
}
