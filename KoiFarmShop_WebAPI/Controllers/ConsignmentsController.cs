using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Services.Interface;
using DataAccessObjects.DTOs.ConsignmentDTO;
using Services.Implement;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsignmentsController : ControllerBase
    {
        private readonly IConsignmentService _consignmentService;

        public ConsignmentsController(IConsignmentService consignmentService)
        {
            _consignmentService = consignmentService;
        }

        // GET: api/Consignments
        [HttpGet]
        public async Task<IActionResult> GetAllConsignments()
        {
            var consignment = await _consignmentService.GetAllConsignments();
            return Ok(consignment);
        }

        // GET: api/Consignments/5
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetConsignmentById(int Id)
        {
            var consignment = await _consignmentService.GetConsignmentById(Id);
            return Ok(consignment);
        }

        [HttpPost]
        public async Task<ActionResult<Consignment>> CreateConsignment(CreateConsignmentDTO createconsignmentDTO)
        {
            var consignment = await _consignmentService.CreateConsignment(createconsignmentDTO);
            return Ok(consignment);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateConsignment(int Id, UpdateConsignmentDTO updateconsignmentDTO)
        {
            var consignment = await _consignmentService.UpdateConsignment(Id, updateconsignmentDTO);
            return Ok(consignment);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreConsignment(int Id)
        {
            await _consignmentService.RestoreConsignment(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteConsignment(int Id)
        {
            var cart = await _consignmentService.DeleteConsignment(Id);
            return Ok();
        }

    }
}
