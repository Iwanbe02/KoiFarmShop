using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResDto
{
    public class LoginResDto
    {
        public required string Token { get; set; }
        public required string Fullname { get; set; }
        public required string Email { get; set; }
    }
}
