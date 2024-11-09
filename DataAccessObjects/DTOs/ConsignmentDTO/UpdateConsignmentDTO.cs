using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.ConsignmentDTO
{
    public class UpdateConsignmentDTO
    {
        public decimal? Price { get; set; }
        public string? Status { get; set; } = string.Empty;
        public List<IFormFile> Img { get; set; }
    }
}
