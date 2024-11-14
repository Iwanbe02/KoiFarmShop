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
        public async Task<List<Image>> GetByKoiFishyIdAsync(int koiFishyId)
        {
            return await _dbContext.Images
                .Where(img => img.KoiFishyId == koiFishyId && img.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<List<Image>> GetByKoiIdAsync(int koiId)
        {
            return await _dbContext.Images
                .Where(img => img.KoiId == koiId && img.IsDeleted == false)
                .ToListAsync();
        }
        public async Task<List<Image>> GetByConsignmentIdAsync(int consignmentId)
        {
            return await _dbContext.Images
                .Where(img => img.ConsignmentId == consignmentId && img.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<Image?> GetByCertificateIdAsync(int originCertificateId)
        {
            return await _dbContext.Images
                .FirstOrDefaultAsync(img => img.OriginCertificateId == originCertificateId && img.IsDeleted == false);
        }
    }
}
