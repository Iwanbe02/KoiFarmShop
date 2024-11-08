using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.KoiFishDTO
{
    public class UpdateKoiFishDTO
    {
        public int CategoryId { get; set; }

        public decimal? Price { get; set; }

        public string Origin { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public int Age { get; set; }

        public double Size { get; set; }

        public string Species { get; set; } = null!;

        public string Character { get; set; } = null!;

        public double AmountFood { get; set; }

        public double ScreeningRate { get; set; }

        public int Amount { get; set; }

        public string Type { get; set; } = null!;

        public IFormFile Img { get; set; }
         public string? Status { get; set; }

    }
}
