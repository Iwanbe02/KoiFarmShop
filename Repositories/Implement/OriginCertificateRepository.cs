using BusinessObjects.Models;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class OriginCertificateRepository : GenericRepository<OriginCertificate>, IOriginCertificateRepository
    {
        public OriginCertificateRepository(KoiFarmShopContext context) : base(context)
        {
        }
    }
}
