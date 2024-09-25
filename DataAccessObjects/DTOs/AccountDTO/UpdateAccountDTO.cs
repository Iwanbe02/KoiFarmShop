using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.AccountDTO
{
    public class UpdateAccountDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? RoleId { get; set; }

        public string? Gender { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? Point { get; set; }

        public string? Status { get; set; }
    }
}
