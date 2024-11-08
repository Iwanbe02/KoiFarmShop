using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        public Task<Image?> GetByKoiFishyIdAsync(int koiFishyId);
        public  Task<Image?> GetByKoiIdAsync(int koiId);
        public Task<Image?> GetByConsignmentIdAsync(int consignmentId);
    }
}
