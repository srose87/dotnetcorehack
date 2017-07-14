using System.Collections.Generic;
using System.Linq;
using dotnetcorehack.Models;

namespace dotnetcorehack.Repositories {

    public class ContactRepository : IContactRepository {
        private DataContext _dataContact;
        public ContactRepository(DataContext dataContext) {
            _dataContact = dataContext;
        }
        public Contact GetContactById(int id) {
            return _dataContact.Contacts.FirstOrDefault(t => t.id == id);
        }

        public List<Contact> GetContacts() {
            return _dataContact.Contacts.ToList();
        }

        public Contact updateContact(int id, Contact contact) {
            var contactToUpdate = _dataContact.Contacts.FirstOrDefault(n => n.id == id);

            if (contactToUpdate == null) {
                return null;
            }

            contactToUpdate.firstName = contact.firstName;
            contactToUpdate.lastName = contact.lastName;
            contactToUpdate.phone = contact.phone;    

            _dataContact.Contacts.Update(contactToUpdate);
            _dataContact.SaveChanges();

            return contactToUpdate;
        }

        public Contact createContact(Contact contact) {
            _dataContact.Contacts.Add(contact);
            _dataContact.SaveChanges();

            return contact;
        }

        public void deleteContact(int id) {
            _dataContact.Contacts.Remove(_dataContact.Contacts.FirstOrDefault(n => n.id == id));
            _dataContact.SaveChanges();
        }
    }

    public interface IContactRepository {
        Contact GetContactById(int id);
        List<Contact> GetContacts();
        Contact updateContact(int id, Contact contact);
        Contact createContact(Contact contact);
        void deleteContact(int id);
    }
    
}
