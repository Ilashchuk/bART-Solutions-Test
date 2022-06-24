using bART_Solutions_test.Data;
using bART_Solutions_test.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace bART_Solutions_test.Services
{
    public class DbServices : IAccountsControllerService, IContactsControllerService
    {
        private readonly bARTSolutionsContext _context;

        public DbServices(bARTSolutionsContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetAccountById(int id) => await _context.Accounts.FindAsync(id);
        public async Task<Account?> GetAccountByName(string? name) => await _context.Accounts.FirstOrDefaultAsync(x => x.Name == name);

        public async Task<Contact?> GetContactById(int id) => await _context.Contacts.FindAsync(id);
        public async Task<Contact?> GetContactByEmail(string? email) => await _context.Contacts.FirstOrDefaultAsync(x => x.Email == email);
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
