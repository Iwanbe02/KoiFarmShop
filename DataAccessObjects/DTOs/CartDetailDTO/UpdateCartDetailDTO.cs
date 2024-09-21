using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.CartDetailDTO
{
    public class UpdateCartDetailDTO
    {
        public int? KoiId { get; set; }

        public int? CartId { get; set; }

        public int? FeedbackId { get; set; }

        public decimal? Price { get; set; }

        public string? Status { get; set; }
    }
}
