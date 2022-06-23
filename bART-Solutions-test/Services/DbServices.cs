using bART_Solutions_test.Data;
using bART_Solutions_test.Models;

namespace bART_Solutions_test.Services
{
    public class DbServices : IAccountsControllerService, IContactsControllerService
    {
        private readonly bARTSolutionsContext _context;

        public DbServices(bARTSolutionsContext context)
        {
            _context = context;
        }

        public Contact? GetContactById(int id) => _context.Contacts.FirstOrDefault(x => x.Id == id);
        public Contact? GetContactByEmail(string? email) => _context.Contacts.FirstOrDefault(x => x.Email == email);
        public Account? GetAccountById(int id) => _context.Accounts.FirstOrDefault(x => x.Id == id);
        public Account? GetAccountByName(string? name) => _context.Accounts.FirstOrDefault(x => x.Name == name);
        public bool IsInDb(Contact? contact)
        {
            if (_context.Contacts.First(c => c.Email == contact.Email) == null)
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
