using bART_Solutions_test.Data;
using bART_Solutions_test.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Threading.Tasks;

namespace bART_Solutions_test.Services
{
    public class DbServices : IAccountsControllerService, IContactsControllerService
    {
        private readonly bARTSolutionsContext _context;

        public DbServices(bARTSolutionsContext context)
        {
            _context = context;
        }

        public Task<Account?> GetAccountByIdAsync(int id) => _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        public Task<Account?> GetAccountByNameAsync(string? name) => _context.Accounts.FirstOrDefaultAsync(x => x.Name == name);

        public Task<Contact?> GetContactByIdAsync(int id) => _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        public Task<Contact?> GetContactByEmailAsync(string? email) => _context.Contacts.FirstOrDefaultAsync(x => x.Email == email);
        public bool IsInDb(Contact? contact)
        {
            if (_context.Contacts.FirstOrDefault(c => c.Email == contact.Email) == null)
            {
                return false;
            }
            return true;
        }
        public void ChangeFirstNameAndLastName(Contact? contact)
        {
            _context.Contacts.First(c => c.Email == contact.Email).FirstName = contact.FirstName;
            _context.Contacts.First(c => c.Email == contact.Email).LastName = contact.LastName;
            _context.SaveChanges();
        }
    }
}
