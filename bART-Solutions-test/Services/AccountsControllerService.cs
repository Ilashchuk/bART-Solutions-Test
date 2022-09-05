using bART_Solutions_test.Data;
using bART_Solutions_test.Models;
using Microsoft.EntityFrameworkCore;

namespace bART_Solutions_test.Services
{
    public class AccountsControllerService : IAccountsControllerService
    {
        public readonly bARTSolutionsContext _context;

        public AccountsControllerService(bARTSolutionsContext context)
        {
            _context = context;
        }

        public Task<List<Account>> GetAccountsAsync()
        {
            return _context.Accounts.Include(a => a.Incidents).ToListAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
             _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public Task<Account?> GetAccountByIdAsync(int id) => _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        public Task<Account?> GetAccountByNameAsync(string? name) => _context.Accounts.FirstOrDefaultAsync(x => x.Name == name);

        public async Task AddNewAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Account account)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }
        public bool AccountExists(int id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
