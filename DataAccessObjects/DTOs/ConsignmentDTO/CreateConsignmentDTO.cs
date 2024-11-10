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

        public int? PaymentId { get; set; }

        public decimal Price { get; set; }
        public List<IFormFile> Img { get; set; }

    }
}
