using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.OrderDTO
{
    public class UpdateOrderDTO
    {
        public string? Status { get; set; }

        public bool? Type { get; set; }

        public decimal Price { get; set; }
    }
}
