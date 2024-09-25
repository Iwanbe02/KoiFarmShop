﻿using BusinessObjects.Models;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class HealthCertificateRepository : GenericRepository<HealthCertificate>, IHealthCertificateRepository
    {
        public HealthCertificateRepository(KoiFarmShopContext context): base(context) 
        {

        }
    }
}
