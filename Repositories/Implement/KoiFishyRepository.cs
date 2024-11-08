using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class KoiFishyRepository : GenericRepository<KoiFishy>, IKoiFishyRepository
    {        
        public KoiFishyRepository(KoiFarmShopContext context) : base(context)
        {
        }       
    }
}
