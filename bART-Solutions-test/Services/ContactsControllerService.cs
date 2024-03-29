﻿using bART_Solutions_test.Data;
using bART_Solutions_test.Models;
using Microsoft.EntityFrameworkCore;

namespace bART_Solutions_test.Services
{
    public class ContactsControllerService : IContactsControllerService
    {
        private readonly bARTSolutionsContext _context;
        public ContactsControllerService(bARTSolutionsContext context)
        {
            _context = context;
        }
        public Task<List<Contact>> GetContactsAsync()
        {
            return _context.Contacts.Include(a => a.Accounts).ToListAsync();
        }
        public Task<Contact?> GetContactByIdAsync(int id) => _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        public Task<Contact?> GetContactByEmailAsync(string? email) => _context.Contacts.FirstOrDefaultAsync(x => x.Email == email);
        public async Task UpdateContactAsync(Contact contact)
        {
            _context.Entry(contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task AddNewContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteContactAsync(Contact contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
        public bool IsContactInDb(Contact? contact)
        {
            if (contact != null && _context.Contacts.FirstOrDefault(c => c.Email == contact.Email) == null)
            {
                return false;
            }
            return true;
        }
        public async Task ChangeFirstNameAndLastNameInContact(Contact? contact)
        {
            if (contact != null)
            {
                _context.Contacts.First(c => c.Email == contact.Email).FirstName = contact.FirstName;
                _context.Contacts.First(c => c.Email == contact.Email).LastName = contact.LastName;
            }
            await _context.SaveChangesAsync();
        }

        public bool ContactExists(int id)
        {
            return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
