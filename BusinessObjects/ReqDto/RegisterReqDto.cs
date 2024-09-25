using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ReqDto
{
    public class RegisterReqDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "Password must be at least 8 characters long, and contain both uppercase and lowercase letters.")]
        public required string Password { get; set; }

        [Required]
        public required string FullName { get; set; }

        public string? Gender { get; set; }

        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must be exactly 10 digits and start with 0.")]
        public string? Phone { get; set; }

        public string? Address { get; set; }

        [CustomDateOfBirthValidation(ErrorMessage = "Date of birth cannot be in the future.")]
        public DateTime? DateOfBirth { get; set; }
    }

    public class CustomDateOfBirthValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dateOfBirth = value as DateTime?;
            if (dateOfBirth.HasValue && dateOfBirth.Value.Date > DateTime.Now.Date)
            {
                return new ValidationResult("Date of birth cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}
