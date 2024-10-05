using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.PromotionDTO
{
    public class UpdatePromotionDTO
    {
        public int? Point { get; set; }

        public double? DiscountPercentage { get; set; }

        public string? Status { get; set; }
    }
}
