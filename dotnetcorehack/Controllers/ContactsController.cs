namespace Dotnetcorehack.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Dotnetcorehack.Factories;
    using Dotnetcorehack.Models;
    using Dotnetcorehack.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Newtonsoft.Json;

    [Route("/[controller]")]
    public class ContactsController : Controller
    {
        private IContactRepository contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.Json(this.contactRepository.GetContacts());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var contact = this.contactRepository.GetContactById(id);
            if (contact == null)
            {
                return this.NotFound();
            }

            return this.Json(contact);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Contact contact)
        {
            if (this.ModelState.IsValid)
            {
                this.contactRepository.CreateContact(contact);

                return this.Created("/contacts/" + contact.Id.ToString(), contact);
            }

            return this.BadRequest(ErrorFactory.GetErrorMessages(this.ViewData.ModelState));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Contact contact)
        {
            if (this.ModelState.IsValid)
            {
                var contactUpdated = this.contactRepository.UpdateContact(id, contact);

                if (contactUpdated == null)
                {
                    return this.NotFound();
                }

                return this.NoContent();
            }

            return this.BadRequest(ErrorFactory.GetErrorMessages(this.ViewData.ModelState));
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.contactRepository.DeleteContact(id);
        }
    }
}
