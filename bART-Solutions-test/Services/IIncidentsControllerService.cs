using bART_Solutions_test.Models;

namespace bART_Solutions_test.Services
{
    public interface IIncidentsControllerService
    {
        public Task<List<Incident>> GetIncidentsAsync();
        public Task<Incident?> GetIncidentByNameAsync(string? name);
        public Task UpdateIncidentAsync(Incident incident);
        public Task AddNewIncidentAsync(Incident incident);
        public Task DeleteIncidentAsync(Incident incident);
        public bool IncidentExists(string id);
    }
}
