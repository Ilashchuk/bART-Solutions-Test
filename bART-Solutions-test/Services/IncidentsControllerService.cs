using bART_Solutions_test.Data;
using bART_Solutions_test.Models;
using Microsoft.EntityFrameworkCore;

namespace bART_Solutions_test.Services
{
    public class IncidentsControllerService : IIncidentsControllerService
    {
        private readonly bARTSolutionsContext _context;
        public IncidentsControllerService(bARTSolutionsContext context)
        {
            _context = context;
        }

        public Task<List<Incident>> GetIncidentsAsync()
        {
            return _context.Incidents.ToListAsync();
        }
        public Task<Incident?> GetIncidentByNameAsync(string? name) => _context.Incidents.FirstOrDefaultAsync(x => x.Name == name);
        public async Task UpdateIncidentAsync(Incident incident)
        {
            _context.Entry(incident).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task AddNewIncidentAsync(Incident incident)
        {
            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteIncidentAsync(Incident incident)
        {
            _context.Incidents.Remove(incident);
            await _context.SaveChangesAsync();
        }

        public bool IncidentExists(string id)
        {
            return (_context.Incidents?.Any(e => e.Name == id)).GetValueOrDefault();
        }
    }
}
