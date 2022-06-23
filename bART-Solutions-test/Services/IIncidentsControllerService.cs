using bART_Solutions_test.Models;

namespace bART_Solutions_test.Services
{
    public interface IIncidentsControllerService
    {
        public Incident? ChangingBeforAddingToDB(Incident incident);
    }
}
