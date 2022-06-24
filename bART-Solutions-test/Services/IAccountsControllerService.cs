using bART_Solutions_test.Models;

namespace bART_Solutions_test.Services
{
    public interface IAccountsControllerService
    {
        public Task<Account?> GetAccountById(int id);
        public Task<Account?> GetAccountByName(string? name);
    }
}
