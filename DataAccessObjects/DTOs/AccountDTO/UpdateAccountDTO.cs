using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.AccountDTO
{
    public class UpdateAccountDTO
    {
        public int RoleId { get; set; }

        public string Name { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string? Email { get; set; }

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public DateTime? Date { get; set; }

        public int Point { get; set; }

        public bool? IsDelete { get; set; }
    }
}
