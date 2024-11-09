using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.CartDTO
{
    public class CreateCartDTO
    {
        public decimal? Price { get; set; }

        public decimal? TotalPrice { get; set; }

        public int? Quantity { get; set; }

        public string? Status { get; set; }
    }
}
