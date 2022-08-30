using bART_Solutions_test.Models;

namespace bART_Solutions_test.Services
{
    public interface IAccountsControllerService
    {
        public Task<Account?> GetAccountByIdAsync(int id);
        public Task<Account?> GetAccountByNameAsync(string? name);
    }
}
