using BusinessObjects.Models;
using DataAccessObjects.DTOs.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> GetByEmailAsync(string email);
        Task<Account> GetByUsernameAsync(string username);
        Task<Account> GetAccountById(int id);
        Task<IEnumerable<Account>> GetAccountByUsername(string username);
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task<Account> CreateAccount(Account account);
        Task<Account> UpdateAccount(int id);
        Task<Account> DeleteAccount(int id);

    }
}
