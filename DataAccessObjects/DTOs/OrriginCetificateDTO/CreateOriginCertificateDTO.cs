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

        public int? OrderId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

    }
}
