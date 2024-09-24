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
    public class UserRepository : IUserRepository
    {
        private readonly KoiFarmShopContext _koi;

        public UserRepository(KoiFarmShopContext koi)
        {
            _koi = koi;
        }
        public void AddUser(Account account)
        {
            _koi.Accounts.Add(account);
            _koi.SaveChanges();
        }

        public Account GetByEmail(string email)
        {
            return _koi.Accounts.SingleOrDefault(u => u.Email == email);
        }
    }
}
