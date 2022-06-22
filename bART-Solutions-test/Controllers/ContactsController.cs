using bART_Solutions_test.Data;
using bART_Solutions_test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace bART_Solutions_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly bARTSolutionsContext _context;
        public ContactsController(bARTSolutionsContext context) => _context = context;
        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
            => await _context.Contacts.ToListAsync();

        [HttpGet("id")]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            return contact == null ? NotFound() : Ok(contact);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
            }
            return BadRequest(ModelState);
        }
    }
}
