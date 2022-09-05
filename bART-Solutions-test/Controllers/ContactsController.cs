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
    public class ContactsController : ControllerBase
    {
        private readonly IContactsControllerService _contactsControleService;

        public ContactsController(IContactsControllerService contactsControleService)
        {
            _contactsControleService = contactsControleService;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _contactsControleService.GetContactsAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
          if (_contactsControleService.GetContactsAsync() == null)
          {
              return NotFound();
          }
            var contact = await _contactsControleService.GetContactByIdAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
            if (id != contact.Id || contact.Accounts == null)
            {
                return BadRequest();
            }

            await _contactsControleService.UpdateContactAsync(contact);

            if (!_contactsControleService.ContactExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _contactsControleService.AddNewContactAsync(contact);

                return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            if (_contactsControleService.GetContactsAsync == null)
            {
                return NotFound();
            }
            var contact = await _contactsControleService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            await _contactsControleService.DeleteContactAsync(contact);

            return NoContent();
        }

        
    }
}
