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
        public string? Name { get; set; }
        public string? Origin { get; set; }

        public string? Gender { get; set; }

        public int? YearOfBirth { get; set; }

        public double? Size { get; set; }

        public string? Variety { get; set; }

        public string? Character { get; set; }

        public string? Diet { get; set; }

        public double? AmountFood { get; set; }

        public double? ScreeningRate { get; set; }

        public string? Type { get; set; }
        public decimal? Price { get; set; }
        public string? Status { get; set; }
        public List<IFormFile> Img { get; set; }

    }
}
