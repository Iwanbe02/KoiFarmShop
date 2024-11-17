using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.OrriginCetificateDTO
{
    public class UpdateOriginCertificateDTO
    {
        public string? Variety { get; set; }

        public string? Gender { get; set; }

        public double? Size { get; set; }

        public int? YearOfBirth { get; set; }

        public DateTime? Date { get; set; }

        public string? PlaceOfIssue { get; set; }
        public IFormFile? Img { get; set; }

    }
}
