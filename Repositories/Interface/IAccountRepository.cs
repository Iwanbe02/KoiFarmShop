using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> Login(string email, string password);
        Task<Account> GetByEmailAsync(string email);
    }
}
