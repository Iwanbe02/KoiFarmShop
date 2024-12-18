﻿using BusinessObjects.Models;
using DataAccessObjects.DTOs.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account> GetAccountById(int id);
        Task<Account> CreateAccount(CreateAccountDTO createAccount);
        Task<Account> UpdateAccount(int id, UpdateAccountDTO updateAccount);
        Task<Account> DeleteAccount(int id);
        Task<Account> RestoreAccount(int id);
        Task<Account> Login(string email, string password);
    }
}
