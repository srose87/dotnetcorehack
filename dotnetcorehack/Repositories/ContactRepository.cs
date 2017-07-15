namespace Dotnetcorehack.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Dotnetcorehack.Models;

    public class ContactRepository : IContactRepository
    {
        private DataContext dataContact;

        public ContactRepository(DataContext dataContext)
        {
            this.dataContact = dataContext;
        }

        public Contact GetContactById(int id)
        {
            return this.dataContact.Contacts.FirstOrDefault(t => t.Id == id);
        }

        public List<Contact> GetContacts()
        {
            return this.dataContact.Contacts.ToList();
        }

        public Contact UpdateContact(int id, Contact contact)
        {
            var contactToUpdate = this.dataContact.Contacts.FirstOrDefault(n => n.Id == id);

            if (contactToUpdate == null)
            {
                return null;
            }

            contactToUpdate.FirstName = contact.FirstName;
            contactToUpdate.LastName = contact.LastName;
            contactToUpdate.Phone = contact.Phone;

            this.dataContact.Contacts.Update(contactToUpdate);
            this.dataContact.SaveChanges();

            return contactToUpdate;
        }

        public Contact CreateContact(Contact contact)
        {
            this.dataContact.Contacts.Add(contact);
            this.dataContact.SaveChanges();

            return contact;
        }

        public void DeleteContact(int id)
        {
            this.dataContact.Contacts.Remove(this.dataContact.Contacts.FirstOrDefault(n => n.Id == id));
            this.dataContact.SaveChanges();
        }
    }
}
