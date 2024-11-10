using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly KoiFarmShopContext _context;
        public AccountRepository(KoiFarmShopContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Account> Login(string email, string password)
        {
            var user = await _context.Accounts.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
            // Query the database for an Account with the provided email
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
