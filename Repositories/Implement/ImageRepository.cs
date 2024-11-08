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
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly KoiFarmShopContext _dbContext;
        public ImageRepository(KoiFarmShopContext context) : base(context)
        {
            this._dbContext = context;
        }
        public async Task<Image?> GetByKoiFishyIdAsync(int koiFishyId)
        {
            return await _dbContext.Images
                .FirstOrDefaultAsync(img => img.KoiFishyId == koiFishyId && img.IsDeleted == false);
        }
        public async Task<Image?> GetByKoiIdAsync(int koiId)
        {
            return await _dbContext.Images
                .FirstOrDefaultAsync(img => img.KoiId == koiId && img.IsDeleted == false);
        }
        public async Task<Image?> GetByConsignmentIdAsync(int consignmentId)
        {
            return await _dbContext.Images
                .FirstOrDefaultAsync(img => img.ConsignmentId == consignmentId && img.IsDeleted == false);
        }
    }
}
