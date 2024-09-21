using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.PaymentDTO
{
    public class UpdatePaymentDTO
    {
        public string? PaymentMethod { get; set; }

        public string? Status { get; set; }
    }
}
