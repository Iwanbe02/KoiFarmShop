using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.ConsignmentDTO
{
    public class CreateConsignmentDTO
    {
        public int? AccountId { get; set; }
        public string? Name { get; set; }

        public int? YearOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Origin { get; set; }

        public string? Variety { get; set; }

        public string? Character { get; set; }

        public double? Size { get; set; }

        public double? AmountFood { get; set; }
        public List<IFormFile> Img { get; set; }

    }
}
