using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.RewardCertificateDTO
{
    public class CreateRewardCertificateDTO
    {
        public int? KoiId { get; set; }

        public int? OrderId { get; set; }

        public string? Description { get; set; }
    }
}
