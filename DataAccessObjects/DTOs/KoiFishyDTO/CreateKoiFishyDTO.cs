﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.KoiFishyDTO
{
    public class CreateKoiFishyDTO
    {
        public int? CategoryId { get; set; }

        public int? Quantity { get; set; }

        public string? Status { get; set; }
    }
}
