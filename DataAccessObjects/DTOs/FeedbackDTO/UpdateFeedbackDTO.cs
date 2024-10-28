using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.FeedbackDTO
{
    public class UpdateFeedbackDTO
    {
        public int? AccountId { get; set; }

        public string? Status { get; set; }

        public string? Description { get; set; }

        public double? Rating { get; set; }
    }
}
