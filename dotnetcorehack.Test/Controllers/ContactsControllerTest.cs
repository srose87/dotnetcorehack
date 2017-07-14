using System;
using Moq;
using Xunit;
using dotnetcorehack.Repositories;
using dotnetcorehack.Controllers;
using Microsoft.AspNetCore.Mvc;
using dotnetcorehack.Models;
using System.Collections.Generic;

namespace dotnetcorehack.Test.Controllers
{
    public class ContactsControllerTest
    {
        private Mock<IContactRepository> _contactRepo;
        private ContactsController _controller;
        private int _expectedId;
        private Random _random;

        public ContactsControllerTest()
        {
            _random = new Random();
            _expectedId = _random.Next();
            _contactRepo = new Mock<IContactRepository>();

            _controller = new ContactsController(_contactRepo.Object);
        }

        private string AnyString()
        {
            return Guid.NewGuid().ToString();
        }

        private void GivenModelIsInvalid()
        {
            _controller.ModelState.AddModelError(AnyString(), AnyString());
        }

        public class DeleteTest : ContactsControllerTest
        {
            [Fact]
            public void ShouldCallRepositoryWithId()
            {
                _controller.Delete(_expectedId);

                _contactRepo.Verify(repo => repo.deleteContact(_expectedId), Times.Once);
            }
        }

        public class PutTest : ContactsControllerTest
        {
            [Fact]
            public void ShouldReturnBadRequestWhenGivenInvalidModel()
            {
                GivenModelIsInvalid();

                var actualResponse = _controller.Put(_random.Next(), null);

                Assert.IsType<BadRequestObjectResult>(actualResponse);
            }

            [Fact]
            public void ShouldReturnNoContentWhenContactUpdates()
            {
                var contact = new Contact();
                _contactRepo.Setup(repo => repo.updateContact(_expectedId, contact)).Returns(new Contact());

                var actualResponse = _controller.Put(_expectedId, contact);

                Assert.IsType<NoContentResult>(actualResponse);
            }

            [Fact]
            public void ShouldReturnNotFoundWhenContactIsValidButDoesNotExist()
            {
                var actualResponse = _controller.Put(_expectedId, new Contact());

                Assert.IsType<NotFoundResult>(actualResponse);
            }
        }

        public class PostTest : ContactsControllerTest
        {
            [Fact]
            public void ShouldCallRepoWithContact()
            {
                var contact = new Contact();
                _contactRepo.Setup(repo => repo.createContact(contact)).Callback(() => contact.id = _expectedId);

                var actualResponse = _controller.Post(contact) as CreatedResult;

                Assert.NotNull(actualResponse);
                Assert.Equal(actualResponse.Location, "/contacts/" + _expectedId);
            }

            [Fact]
            public void ShouldReturnBadRequestWhenModelIsInvalid()
            {
                GivenModelIsInvalid();

                var actualResponse = _controller.Post(null);

                Assert.IsType<BadRequestObjectResult>(actualResponse);
            }
        }

        public class GetTest : ContactsControllerTest
        {
            private List<T> N<T>(int count, Func<T> creator)
            {
                var list = new List<T>();

                for(var index = 0; index < count; index++)
                {
                    list.Add(creator());
                }

                return list;
            }

            [Fact]
            public void ShouldReturnAllContacts()
            {
                var expectedContactsCount = _random.Next(0, 100);
                var expectedContacts = N(expectedContactsCount, () => new Contact() { id = _random.Next()});
                _contactRepo.Setup(repo => repo.GetContacts()).Returns(expectedContacts);

                var response = _controller.Get() as JsonResult;
                var actualContacts = response.Value as List<Contact>;

                Assert.Same(expectedContacts, actualContacts);
                Assert.Equal(expectedContactsCount, actualContacts.Count);
            }

            [Fact]
            public void ShouldReturnSpecificContact()
            {
                var expectedContact = new Contact();
                _contactRepo.Setup(repo => repo.GetContactById(_expectedId)).Returns(expectedContact);

                var response = _controller.Get(_expectedId) as JsonResult;

                Assert.Same(expectedContact, response.Value);
            }

            [Fact]
            public void ShouldReturnNotFoundIfContactDoesNotExist()
            {
                var response = _controller.Get(_expectedId);

                Assert.IsType<NotFoundResult>(response);
            }
        }

    }
}
