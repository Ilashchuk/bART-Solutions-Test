using bART_Solutions_test.Models;

namespace bART_Solutions_test.Services
{
    public interface IContactsControllerService
    {
        public Contact? GetContactById(int id);
        public Contact? GetContactByEmail(string? email);
        public bool IsInDb(Contact? contact);
        public void ChangeFirstNameAndLastName(Contact? contact);

    }
}
