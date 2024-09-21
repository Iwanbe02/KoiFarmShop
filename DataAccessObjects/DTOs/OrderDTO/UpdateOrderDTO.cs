using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.OrderDTO
{
    public class UpdateOrderDTO
    {
        public int? KoiId { get; set; }

        public int? KoiFishyId { get; set; }

        public int? AccountId { get; set; }

        public int? PaymentId { get; set; }

        public string? Status { get; set; }

        public bool? Type { get; set; }

        public decimal? Price { get; set; }
    }
}
