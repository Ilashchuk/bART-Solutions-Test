using bART_Solutions_test.Models;

namespace bART_Solutions_test.Services
{
    public interface IContactsControllerService
    {
        public Task<List<Contact>> GetContactsAsync();
        public Task<Contact?> GetContactByIdAsync(int id);
        public Task<Contact?> GetContactByEmailAsync(string? email);
        public Task UpdateContactAsync(Contact contact);
        public Task AddNewContactAsync(Contact contact);
        public Task DeleteContactAsync(Contact contact);
        public bool IsContactInDb(Contact? contact);
        public void ChangeFirstNameAndLastNameInContact(Contact? contact);
        public bool ContactExists(int id);

    }
}
