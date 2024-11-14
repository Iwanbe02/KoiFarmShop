using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.CartDTO
{
    public class CreateCartDTO
    {
        public int? KoiId { get; set; }

        public int? KoiFishyId { get; set; }

        public decimal? Price { get; set; }
    }
}
