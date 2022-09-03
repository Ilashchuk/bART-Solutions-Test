using bART_Solutions_test.Models;

namespace bART_Solutions_test.Services
{
    public interface IAccountsControllerService
    {
        public Task<List<Account>> GetAccountsAsync();
        public Task<Account?> GetAccountByIdAsync(int id);
        public Task<Account?> GetAccountByNameAsync(string? name);
        public Task UpdateAccountAsync(Account account);
        public Task AddNewAccountAsync(Account account);
        public Task DeleteAccountAsync(Account account);

        public bool AccountExists(int id);
    }
}
