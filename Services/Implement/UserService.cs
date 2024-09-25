using BusinessObjects.Enums;
using BusinessObjects.Models;
using BusinessObjects.ReqDto;
using BusinessObjects.ResDto;
using BusinessObjects.ReturnCommon;
using DataAccessObjects.DTOs.AccountDTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public Return<LoginResDto> Login(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return new Return<LoginResDto>
            {
                Data = new LoginResDto
                {
                    Token = GenerateJwtToken(user),
                    Fullname = user.Name,
                    Email = user.Email
                },
                Message = "Login Successfully"
            };
        }

        public void Register(RegisterReqDto createAccountDTO)
        {
            if (_userRepository.GetByEmail(createAccountDTO.Email) != null)
            {
                throw new Exception("Email already exists.");
            }

            var newUser = new Account
            {
                Name = createAccountDTO.FullName,
                Email = createAccountDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(createAccountDTO.Password),
                RoleId = 1,  // Default role
                DateOfBirth = createAccountDTO.DateOfBirth,
                Phone = createAccountDTO.Phone,
                Address = createAccountDTO.Address,
                Status = StatusEnums.ACTIVE,
                Point = 0
            };

            _userRepository.AddUser(newUser);
        }

        private string GenerateJwtToken(Account user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
