using BusinessObjects.Models;
using DataAccessObjects.DTOs.AccountDTO;
using DataAccessObjects.DTOs.KoiFishDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Implement;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var account = await _accountService.GetAllAccounts();
            return Ok(account);
        }

        // GET: api/Accounts/5
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAccountById(int Id)
        {
            var account = await _accountService.GetAccountById(Id);
            return Ok(account);
        }


        // POST: api/Accounts

        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount(CreateAccountDTO createAccountDTO)
        {
            var account = await _accountService.CreateAccount(createAccountDTO);
            return Ok(account);
        }

        // PUT: api/Accounts/5
        [HttpPut("{Id}")]

        public async Task<IActionResult> UpdateAccount(int Id, UpdateAccountDTO updateAccountDTO)
        {
            var account = await _accountService.UpdateAccount(Id, updateAccountDTO);
            return Ok(account);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreAccount(int Id)
        {
            await _accountService.RestoreAccount(Id);
            return Ok();
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAccount(int Id)
        {
            var account = await _accountService.DeleteAccount(Id);
            return Ok(account);
        }

    }
}
