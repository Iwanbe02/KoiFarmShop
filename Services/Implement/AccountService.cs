using BusinessObjects.Models;
using DataAccessObjects.DTOs.AccountDTO;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> CreateAccount(CreateAccountDTO createAccount)
        {
            var account = new Account
            {
                Name = createAccount.Name,
                Gender = createAccount.Gender,
                Email = createAccount.Email,
                Password = createAccount.Password,
                Phone = createAccount.Phone,
                Address = createAccount.Address,
                DateOfBirth = createAccount.DateOfBirth,
                CreatedDate = DateTime.Now
            };
            await _accountRepository.AddAsync(account);
            return account;
        }

        public async Task<Account> DeleteAccount(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if(account == null)
            {
                throw new Exception($"Cart with ID{id} is not found");
            }
            account.IsDeleted = true;
            account.DeletedDate = DateTime.Now;
            await _accountRepository.UpdateAsync(account);
            return account;
        }

        public Task<Account> GetAccountById(int id)
        {
            return _accountRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Account>> GetAllAccounts()
        {
            return _accountRepository.GetAllAsync();
        }

        public async Task<Account> RestoreAccount(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new Exception($"Cart with ID{id} is not found");
            }
            if (account.IsDeleted == true)
            {
                account.IsDeleted = false;
                await _accountRepository.UpdateAsync(account);
            }
            return account;
        }

        public async Task<Account> UpdateAccount(int id, UpdateAccountDTO updateAccount)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null )
            {
                throw new Exception($"Cart with ID{id} is not found");
            }
            account.Name = updateAccount.Name;
            account.Gender = updateAccount.Gender;
            account.Email = updateAccount.Email;
            account.Password = updateAccount.Password;
            account.Phone = updateAccount.Phone;
            account.Address = updateAccount.Address;
            account.DateOfBirth = updateAccount.DateOfBirth;
            account.ModifiedDate = DateTime.Now;

            await _accountRepository.UpdateAsync(account);
            return account;
        }
    }
}
