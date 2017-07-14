using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetcorehack.Repositories;
using dotnetcorehack.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using dotnetcorehack.Factories;

namespace dotnetcorehack.Controllers
{
    [Route("/[controller]")]
    public class ContactsController : Controller
    {
        private IContactRepository _contactRepository;
        public ContactsController(IContactRepository contactRepository) {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {    
            return Json(_contactRepository.GetContacts());
            
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var contact = _contactRepository.GetContactById(id);
            if (contact == null) {
                return NotFound();
            }
            return Json(contact);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Contact contact)
        {
            if (ModelState.IsValid) {
                _contactRepository.createContact(contact);

                return Created("/contacts/" + contact.id.ToString(), contact);
            }

            return BadRequest(ErrorFactory.getErrorMessages(ViewData.ModelState));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Contact contact)
        {
            if (ModelState.IsValid) {                
                var contactUpdated = _contactRepository.updateContact(id, contact);

                if (contactUpdated == null) {
                    return NotFound();
                }

                return NoContent();
            }

            return BadRequest(ErrorFactory.getErrorMessages(ViewData.ModelState));
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _contactRepository.deleteContact(id);
        }
    }
}
