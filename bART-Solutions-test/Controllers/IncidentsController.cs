﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bART_Solutions_test.Data;
using bART_Solutions_test.Models;

namespace bART_Solutions_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly bARTSolutionsContext _context;

        public IncidentsController(bARTSolutionsContext context)
        {
            _context = context;
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Incident>> GetIncident(string id)
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
        public async Task<ActionResult<Incident>> CreateIncident(Incident incident)
        {
          if (_context.Incidents == null)
          {
              return Problem("Entity set 'bARTSolutionsContext.Incidents'  is null.");
          }


            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncident", new { id = incident.Name }, incident);
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
