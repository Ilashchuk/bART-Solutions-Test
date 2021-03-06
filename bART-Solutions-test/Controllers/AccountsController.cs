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
        private readonly bARTSolutionsContext _context;
        private readonly DbServices _services;

        public AccountsController(bARTSolutionsContext context)
        {
            _context = context;
            _services = new DbServices(context);
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
          if (_context.Accounts == null)
          {
              return NotFound();
          }
            return await _context.Accounts.Include(a => a.Incidents).ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
          if (_context.Accounts == null)
          {
              return NotFound();
          }
            var account = await _context.Accounts.Include(a => a.Incidents)
                .SingleOrDefaultAsync(a => a.Id == id);

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

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Account>> CreateAccount(Account account)
        {
            if (_context.Accounts == null)
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
            if (_services.IsInDb(account.Contact))
            {
                _services.ChangeFirstNameAndLastName(account.Contact);
            }
            else if(account.Contact != null)
            {
                _context.Contacts.AddRange(account.Contact);

                _context.SaveChanges();
            }
            //link account to contact
            if (account.Contact == null)
            {
                account.Contact = await _services.GetContactById(account.ContactId);
            }
            if (account.ContactId == 0)
            {
                account.ContactId = _services.GetContactByEmail(account.Contact.Email).Id;
            }
            //add new Account to DB
            _context.Accounts.AddRange(new Account { 
                Name = account.Name,
                ContactId = account.ContactId,
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
