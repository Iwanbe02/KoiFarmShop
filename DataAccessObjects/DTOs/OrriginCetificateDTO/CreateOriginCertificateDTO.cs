using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.OrriginCetificateDTO
{
    public class CreateOriginCertificateDTO
    {
        public int? KoiId { get; set; }

        public string? Variety { get; set; }

        public string? Gender { get; set; }

        public double? Size { get; set; }

        public int? YearOfBirth { get; set; }

        public DateTime? Date { get; set; }

        public string? Signature { get; set; }

        public string? Location { get; set; }

    }
}
