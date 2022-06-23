using bART_Solutions_test.Data;
using bART_Solutions_test.Models;

namespace bART_Solutions_test.Services
{
    public class DbServices : IAccountsControllerService, IIncidentsControllerService
    {
        private readonly bARTSolutionsContext _context;

        public DbServices(bARTSolutionsContext context)
        {
            _context = context;
        }

        public Account? ChangingBeforAddingToDB(Account account)
        {
            if (account.ContactId == 0 && account.Contact == null)
            {
                return null;
            }

            if (account.Contact == null)
            {
                account.Contact = _context.Contacts.FirstOrDefault(x => x.Id == account.ContactId);
            }
            else if (account.ContactId == 0)
            {
                Contact? contact = _context.Contacts.FirstOrDefault(x => x.Email == account.Contact.Email);
                if (contact != null)
                {
                    account.ContactId = contact.Id;
                }
                else
                {
                    return null;
                }
            }
            return account;
        }

        public Incident? ChangingBeforAddingToDB(Incident incident)
        {
            if (incident.AccountId == 0 && incident.Account == null)
            {
                return null;
            }

            if (incident.Account == null)
            {
                incident.Account = _context.Accounts.FirstOrDefault(x => x.Id == incident.AccountId);
            }
            else if (incident.AccountId == 0)
            {
                Account? account = _context.Accounts.FirstOrDefault(x => x.Name == incident.Account.Name);
                
                if (account != null)
                {
                    incident.AccountId = account.Id;
                }
                else
                {
                    return null;
                }
            }
            return incident;
        }
    }
}
