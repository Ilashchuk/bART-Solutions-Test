using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bART_Solutions_test.Data;
using bART_Solutions_test.Models;
using bART_Solutions_test.Services;

namespace bART_Solutions_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsControllerService _accountsControllerService;
        private readonly IContactsControllerService _contactsControllerService;

        public AccountsController(IAccountsControllerService accountsControllerService, IContactsControllerService contactsControllerService)
        {
            _accountsControllerService = accountsControllerService;
            _contactsControllerService = contactsControllerService;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await _accountsControllerService.GetAccountsAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            if (_accountsControllerService.GetAccountsAsync() == null)
            {
                return NotFound();
            }

            var account = await _accountsControllerService.GetAccountByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            await _accountsControllerService.UpdateAccountAsync(account);

            if (!_accountsControllerService.AccountExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Account>> CreateAccount(Account account)
        {
            if (_accountsControllerService.GetAccountsAsync == null)
            {
                return Problem("Entity set 'bARTSolutionsContext.Accounts'  is null.");
            }

            if (account == null)
            {
                return BadRequest();
            }
            //if Contact is no specified => return BadRequest(error 400)
            if (account.ContactId == 0 && account.Contact == null)
            {
                return BadRequest();
            }
            //if Contact is in db => update Contact, else => add new Contact
            if (_contactsControllerService.IsContactInDb(account.Contact))
            {
                await _contactsControllerService.ChangeFirstNameAndLastNameInContact(account.Contact);
            }
            else if(account.Contact != null)
            {
                await _contactsControllerService.AddNewContactAsync(account.Contact);
            }
            //link account to contact
            var contact = await _contactsControllerService.GetContactByIdAsync(account.ContactId);
            if (account.Contact == null)
            {
                account.Contact = contact;
            }
            if (account.ContactId == 0 && contact != null)
            {
                account.ContactId = contact.Id;
            }
            //add new Account to DB
            await _accountsControllerService.AddNewAccountAsync(account);

            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (_accountsControllerService.GetAccountsAsync == null)
            {
                return NotFound();
            }
            var account = await _accountsControllerService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            await _accountsControllerService.DeleteAccountAsync(account);

            return NoContent();
        }

        
    }
}
