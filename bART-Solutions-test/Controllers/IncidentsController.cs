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
        private readonly IIncidentsControllerService _incidentsControllerService;
        private readonly IAccountsControllerService _accountsControllerService;
        private readonly IContactsControllerService _contactsControllerService;

        public IncidentsController(
            IIncidentsControllerService incidentsControllerService, 
            IAccountsControllerService accountsControllerService, 
            IContactsControllerService contactsControllerService)
        {
            _incidentsControllerService = incidentsControllerService;
            _accountsControllerService = accountsControllerService;
            _contactsControllerService = contactsControllerService;
        }

        // GET: api/Incidents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incident>>> GetIncidents()
        {
            return await _incidentsControllerService.GetIncidentsAsync();
        }

        // GET: api/Incidents/5
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Incident>> GetIncidentByName(string name)
        {
          if (_incidentsControllerService.GetIncidentsAsync() == null)
          {
              return NotFound();
          }
            var incident = await _incidentsControllerService.GetIncidentByNameAsync(name);

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

            await _incidentsControllerService.UpdateIncidentAsync(incident);

            if (!_incidentsControllerService.IncidentExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Incidents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Incident>> CreateIncident(Incident incident)
        {
          if (_incidentsControllerService.GetIncidentsAsync == null)
          {
              return Problem("Entity set 'bARTSolutionsContext.Incidents'  is null.");
          }
            //if Account is no specified => return BadRequest(error 400)
            if (incident.AccountId == 0 && incident.Account == null)
            {
                return BadRequest();
            }
            //if Account is not found in DB => return NotFound(error 404)
            if (incident.Account == null)
            {
                incident.Account = await _accountsControllerService.GetAccountByIdAsync(incident.AccountId);
            }
            else if (incident.AccountId == 0)
            {
                Account? account = await _accountsControllerService.GetAccountByNameAsync(incident.Account.Name);

                if (account != null)
                {
                    incident.AccountId = account.Id;
                }
                else
                {
                    return NotFound();
                }
            }
            //if Contact is in DB => update Contact
            if (incident.Account != null && _contactsControllerService.IsContactInDb(incident.Account.Contact))
            {
                await _contactsControllerService.ChangeFirstNameAndLastNameInContact(incident.Account.Contact);
                //link account to contact
                Account? account_ = await _accountsControllerService.GetAccountByNameAsync(incident.Account.Name);
                if (account_ != null && incident.Account.Contact != null)
                {
                    account_.Contact = await _contactsControllerService.GetContactByEmailAsync(incident.Account.Contact.Email);
                    account_.ContactId = _contactsControllerService.GetContactByEmailAsync(incident.Account.Contact.Email).Id;

                    await _accountsControllerService.UpdateAccountAsync(account_);
                }
            }
            //create new incident
            if (incident.Account != null)
            {
                Incident newIncident = new()
                {
                    Description = incident.Description,
                    Account = await _accountsControllerService.GetAccountByNameAsync(incident.Account.Name),
                    AccountId = _accountsControllerService.GetAccountByNameAsync(incident.Account.Name).Id
                };

                //add new incident to DB
                await _incidentsControllerService.AddNewIncidentAsync(newIncident);

                return newIncident;
            }
            return BadRequest();
        }

        // DELETE: api/Incidents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncident(string id)
        {
            if (_incidentsControllerService.GetIncidentsAsync() == null)
            {
                return NotFound();
            }

            var incident = await _incidentsControllerService.GetIncidentByNameAsync(id);
            if (incident == null)
            {
                return NotFound();
            }

            await _incidentsControllerService.DeleteIncidentAsync(incident);

            return NoContent();
        }

        
    }
}
