using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.AccountDTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Must be valid email!")]
        public required string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "Password must be at least 8 characters long, and contain both uppercase and lowercase letters.")]
        public required string Password { get; set; }
    }
}
