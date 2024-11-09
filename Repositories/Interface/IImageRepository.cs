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
        Task<List<Image>> GetByKoiFishyIdAsync(int koiFishyId);
        Task<List<Image>> GetByKoiIdAsync(int koiId);
        Task<List<Image>> GetByConsignmentIdAsync(int consignmentId);
    }
}
