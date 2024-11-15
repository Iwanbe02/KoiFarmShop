using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.OrderDTO
{
    public class UpdateOrderDTO
    {
        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Status { get; set; }

        public decimal Price { get; set; }
    }
}
