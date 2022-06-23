using bART_Solutions_test.Models;

namespace bART_Solutions_test.Services
{
    public interface IAccountsControllerService
    {
        public Account? GetAccountById(int id);
        public Account? GetAccountByName(string? name);
    }
}
