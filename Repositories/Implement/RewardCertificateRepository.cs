using BusinessObjects.Models;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class RewardCertificateRepository : GenericRepository<RewardCertificate>, IRewardCertificateRepository
    {
        public RewardCertificateRepository(KoiFarmShopContext context) : base(context)
        {
        }
    }
}
