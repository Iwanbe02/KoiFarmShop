using BusinessObjects.Models;
using DataAccessObjects.DTOs.ConsignmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IConsignmentService
    {
        Task<IEnumerable<Consignment>> GetAllConsignments();

        Task<Consignment> GetConsignmentById(int id);
        Task<Consignment> CreateConsignment(CreateConsignmentDTO createConsignment);
        Task<Consignment> UpdateConsignment(int id, UpdateConsignmentDTO updateConsignment);
        Task<Consignment> UpdateConsignmentStatus(int consignmentId, string newStatus);
        Task<Consignment> DeleteConsignment(int id);
        Task<Consignment> RestoreConsignment(int id);
    }
}
