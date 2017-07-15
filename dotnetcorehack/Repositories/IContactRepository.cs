namespace Dotnetcorehack.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Dotnetcorehack.Models;

    public interface IContactRepository
    {
        Contact GetContactById(int id);

        List<Contact> GetContacts();

        Contact UpdateContact(int id, Contact contact);

        Contact CreateContact(Contact contact);

        void DeleteContact(int id);
    }
}
