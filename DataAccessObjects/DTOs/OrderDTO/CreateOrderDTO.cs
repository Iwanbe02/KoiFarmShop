using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.OrderDTO
{
    public class CreateOrderDTO
    {
        public int? CartId { get; set; }

        public int? AccountId { get; set; }

        public int? PaymentId { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
        public decimal Price { get; set; }
    }
}
