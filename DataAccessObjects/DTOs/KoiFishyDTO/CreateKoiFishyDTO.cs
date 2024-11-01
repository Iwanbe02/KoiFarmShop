using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.KoiFishyDTO
{
    public class CreateKoiFishyDTO
    {
        public int? CategoryId { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public string? Status { get; set; }

        public IFormFile Img { get; set; }
    }
}
