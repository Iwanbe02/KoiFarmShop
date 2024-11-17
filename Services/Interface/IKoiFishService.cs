using BusinessObjects.Models;
using DataAccessObjects.DTOs.KoiFishDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IKoiFishService
    {
        Task<IEnumerable<KoiFish>> GetAllKoiFishes();
        Task<KoiFish> GetKoiFishById(int id);
        Task<KoiFish> CreateKoiFish(CreateKoiFishDTO createKoiFish);
        Task<KoiFish> UpdateKoiFish(int id, UpdateKoiFishDTO updateKoiFish);
        Task<KoiFish> UpdateKoiFishStatus(int koiFishId, string newStatus);
        Task<KoiFish> DeleteKoiFish(int id);
        Task<KoiFish> RestoreKoiFish(int id);
        Task<Dictionary<int, Dictionary<string, decimal>>> GetMonthlyKoiFish();
        Task<int> GetTotalKoiFishByMonth(int month);
        Task<decimal> GetTotalPriceKoiFish();
    }
}
