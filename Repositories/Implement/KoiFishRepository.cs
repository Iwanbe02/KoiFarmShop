using BusinessObjects.Models;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class KoiFishRepository : GenericRepository<KoiFish>, IKoiFishRepository
    {
        private readonly KoiFarmShopContext _dbContext;
        public KoiFishRepository(KoiFarmShopContext context, ICurrentTime currentTime) : base(context, currentTime)
        {
            this._dbContext = context;
        }
    }
}
