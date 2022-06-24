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
    public class IncidentsController : ControllerBase
    {
        private readonly bARTSolutionsContext _context;
        private readonly DbServices _services;

        public IncidentsController(bARTSolutionsContext context)
        {
            _context = context;
            _services = new DbServices(context);
        }

        // GET: api/Incidents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incident>>> GetIncidents()
        {
          if (_context.Incidents == null)
          {
              return NotFound();
          }
            return await _context.Incidents.ToListAsync();
        }

        // GET: api/Incidents/5
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Incident>> GetIncidentByName(string name)
        {
          if (_context.Incidents == null)
          {
              return NotFound();
          }
            var incident = await _context.Incidents.FindAsync(name);

            if (incident == null)
            {
                return NotFound();
            }

            return incident;
        }

        // PUT: api/Incidents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncident(string id, Incident incident)
        {
            if (id != incident.Name)
            {
                return BadRequest();
            }

            _context.Entry(incident).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentExists(id))
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

        // POST: api/Incidents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Incident>> CreateIncident(Incident incident)
        {
          if (_context.Incidents == null)
          {
              return Problem("Entity set 'bARTSolutionsContext.Incidents'  is null.");
          }

            if (incident.AccountId == 0 && incident.Account == null)
            {
                return NotFound();
            }

            if (incident.Account == null)
            {
                incident.Account = _services.GetAccountById(incident.AccountId);
            }
            else if (incident.AccountId == 0)
            {
                Account? account = _services.GetAccountByName(incident.Account.Name);

                if (account != null)
                {
                    incident.AccountId = account.Id;
                }
                else
                {
                    return NotFound();
                }
            }

            if (_services.IsInDb(incident.Account.Contact))
            {
                _services.ChangeFirstNameAndLastName(incident.Account.Contact);
            }

            _context.Accounts.First(x => x.Name == incident.Account.Name).Contact =
                _context.Contacts.First(x => x.Email == incident.Account.Contact.Email);
            _context.Accounts.First(x => x.Name == incident.Account.Name).ContactId =
                _context.Contacts.First(x => x.Email == incident.Account.Contact.Email).Id;

            Incident newIncident = new Incident { Description = incident.Description,
                                                  Account = _context.Accounts.First(x => x.Name == incident.Account.Name),
                                                  AccountId = _context.Accounts.First(x => x.Name == incident.Account.Name).Id
            };

            _context.Incidents.AddRange(newIncident);
            _context.SaveChanges();

            return newIncident;
        }

        // DELETE: api/Incidents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncident(string id)
        {
            if (_context.Incidents == null)
            {
                return NotFound();
            }
            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }

            _context.Incidents.Remove(incident);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncidentExists(string id)
        {
            return (_context.Incidents?.Any(e => e.Name == id)).GetValueOrDefault();
        }
    }
}
