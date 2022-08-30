using bART_Solutions_test.Models;

namespace bART_Solutions_test.Services
{
    public interface IContactsControllerService
    {
        public Task<Contact?> GetContactByIdAsync(int id);
        public Task<Contact?> GetContactByEmailAsync(string? email);
        public bool IsInDb(Contact? contact);
        public void ChangeFirstNameAndLastName(Contact? contact);

    }
}
