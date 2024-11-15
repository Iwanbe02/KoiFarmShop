using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.CartItemDTO
{
    public class CreateCartItemDTO
    {
        public int? CartId { get; set; }

        public int? KoiFishId { get; set; }

        public int? KoiFishyId { get; set; }

        public int? ConsignmentId { get; set; }

        public decimal? Price { get; set; }
    }
}
