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
        Task<KoiFish> DeleteKoiFish(int id);
    }
}
