namespace Dotnetcorehack.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using Dotnetcorehack.Controllers;
    using Dotnetcorehack.Models;
    using Dotnetcorehack.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class ContactsControllerTest
    {
        private Mock<IContactRepository> contactRepository;
        private ContactsController contactsController;
        private int expectedId;
        private Random random;

        public ContactsControllerTest()
        {
            this.random = new Random();
            this.expectedId = this.random.Next();
            this.contactRepository = new Mock<IContactRepository>();

            this.contactsController = new ContactsController(this.contactRepository.Object);
        }

        private string AnyString()
        {
            return Guid.NewGuid().ToString();
        }

        private void GivenModelIsInvalid()
        {
            this.contactsController.ModelState.AddModelError(this.AnyString(), this.AnyString());
        }

        public class DeleteTest : ContactsControllerTest
        {
            [Fact]
            public void ShouldCallRepositoryWithId()
            {
                this.contactsController.Delete(this.expectedId);

                this.contactRepository.Verify(repo => repo.DeleteContact(this.expectedId), Times.Once);
            }
        }

        public class PutTest : ContactsControllerTest
        {
            [Fact]
            public void ShouldReturnBadRequestWhenGivenInvalidModel()
            {
                this.GivenModelIsInvalid();

                var actualResponse = this.contactsController.Put(this.random.Next(), null);

                Assert.IsType<BadRequestObjectResult>(actualResponse);
            }

            [Fact]
            public void ShouldReturnNoContentWhenContactUpdates()
            {
                var contact = new Contact();
                this.contactRepository.Setup(repo => repo.UpdateContact(this.expectedId, contact)).Returns(new Contact());

                var actualResponse = this.contactsController.Put(this.expectedId, contact);

                Assert.IsType<NoContentResult>(actualResponse);
            }

            [Fact]
            public void ShouldReturnNotFoundWhenContactIsValidButDoesNotExist()
            {
                var actualResponse = this.contactsController.Put(this.expectedId, new Contact());

                Assert.IsType<NotFoundResult>(actualResponse);
            }
        }

        public class PostTest : ContactsControllerTest
        {
            [Fact]
            public void ShouldCallRepoWithContact()
            {
                var contact = new Contact();
                this.contactRepository.Setup(repo => repo.CreateContact(contact)).Callback(() => contact.Id = this.expectedId);

                var actualResponse = this.contactsController.Post(contact) as CreatedResult;

                Assert.NotNull(actualResponse);
                Assert.Equal(actualResponse.Location, "/contacts/" + this.expectedId);
            }

            [Fact]
            public void ShouldReturnBadRequestWhenModelIsInvalid()
            {
                this.GivenModelIsInvalid();

                var actualResponse = this.contactsController.Post(null);

                Assert.IsType<BadRequestObjectResult>(actualResponse);
            }
        }

        public class GetTest : ContactsControllerTest
        {
            [Fact]
            public void ShouldReturnAllContacts()
            {
                var expectedContactsCount = this.random.Next(0, 100);
                var expectedContacts = this.N(expectedContactsCount, () => new Contact() { Id = this.random.Next() });
                this.contactRepository.Setup(repo => repo.GetContacts()).Returns(expectedContacts);

                var response = this.contactsController.Get() as JsonResult;
                var actualContacts = response.Value as List<Contact>;

                Assert.Same(expectedContacts, actualContacts);
                Assert.Equal(expectedContactsCount, actualContacts.Count);
            }

            [Fact]
            public void ShouldReturnSpecificContact()
            {
                var expectedContact = new Contact();
                this.contactRepository.Setup(repo => repo.GetContactById(this.expectedId)).Returns(expectedContact);

                var response = this.contactsController.Get(this.expectedId) as JsonResult;

                Assert.Same(expectedContact, response.Value);
            }

            [Fact]
            public void ShouldReturnNotFoundIfContactDoesNotExist()
            {
                var response = this.contactsController.Get(this.expectedId);

                Assert.IsType<NotFoundResult>(response);
            }

            private List<T> N<T>(int count, Func<T> creator)
            {
                var list = new List<T>();

                for (var index = 0; index < count; index++)
                {
                    list.Add(creator());
                }

                return list;
            }
        }
    }
}
