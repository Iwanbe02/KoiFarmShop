using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.ConsignmentDTO
{
    public class UpdateConsignmentDTO
    {
        public int Id { get; set; }

        public int? AccountId { get; set; }

        public int? KoiId { get; set; }

        public int? PaymentId { get; set; }

        public decimal? Price { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? Status { get; set; } = string.Empty;
    }
}
