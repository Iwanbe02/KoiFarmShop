using BusinessObjects.Models;
using DataAccessObjects.DTOs.ConsignmentDTO;
using Microsoft.Identity.Client;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class ConsignmentService : IConsignmentService
    {
        private readonly IConsignmentRepository _consignmentRepository;

        public ConsignmentService(IConsignmentRepository consignmentRepository)
        {
            _consignmentRepository = consignmentRepository;
        }
        public async Task<Consignment> CreateConsignment(CreateConsignmentDTO createConsignment)
        {
            var consignment = new Consignment
            {
                AccountId = createConsignment.AccountId,
                KoiId = createConsignment.KoiId,
                PaymentId = createConsignment.PaymentId,
                StartTime = createConsignment.StartTime,
                EndTime = createConsignment.EndTime,
                Status = createConsignment.Status,

            };
            await _consignmentRepository.AddAsync(consignment);
            return consignment;
        }

        public async Task<Consignment> DeleteConsignment(int id)
        {
            var consignment = await _consignmentRepository.GetByIdAsync(id);
            if (consignment == null)
            {
                throw new Exception($"Cart with ID{id} is not found");
            }
            consignment.IsDeleted = true;
            await _consignmentRepository.UpdateAsync(consignment); 
            return consignment;
        }

        public async Task<IEnumerable<Consignment>> GetAllConsignments()
        {
            return await _consignmentRepository.GetAllAsync();
        }

        public async Task<Consignment> GetConsignmentById(int id)
        {
            return await _consignmentRepository.GetByIdAsync(id);
        }

        public async Task<Consignment> RestoreConsignment(int id)
        {
            var consignment = await _consignmentRepository.GetByIdAsync(id);
            if (consignment == null)
            {
                throw new Exception($"Cart with ID{id} is not found");
            }
            if (consignment.IsDeleted == true)
            {
                consignment.IsDeleted = false;
                await _consignmentRepository.UpdateAsync(consignment);
            }
            return consignment;
        }

        public async Task<Consignment> UpdateConsignment(int id, UpdateConsignmentDTO updateConsignment)
        {
            var consignment = await _consignmentRepository.GetByIdAsync(id);
            if (consignment == null)
            {
                throw new Exception($"Cart with ID{id} is not found");
            }
            consignment.AccountId = updateConsignment.AccountId;
            consignment.KoiId = updateConsignment.KoiId;
            consignment.PaymentId = updateConsignment.PaymentId;
            consignment.StartTime = updateConsignment.StartTime;
            consignment.EndTime = updateConsignment.EndTime;
            consignment.Status = updateConsignment.Status;

            await _consignmentRepository.UpdateAsync(consignment);
            return consignment;
        }
    }
}
