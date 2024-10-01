using BusinessObjects.Models;
using DataAccessObjects.DTOs.AccountDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class AccountRepository : GenericRepository<Account> , IAccountRepository
    {
        private readonly KoiFarmShopContext _db;
      
        public AccountRepository(KoiFarmShopContext context) : base(context)
        {
            _db = context;
        }

        public Task<Account> CreateAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public Task<Account> DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAccountByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            try {
                var accountList = await _db.Accounts
                    .Include(x => x.Role)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToListAsync();
                return accountList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Account> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<Account> UpdateAccount(int id)
        {
            throw new NotImplementedException();
        }
    }
}
