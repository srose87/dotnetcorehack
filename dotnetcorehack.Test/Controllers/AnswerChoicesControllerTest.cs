namespace Dotnetcorehack.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using Dotnetcorehack.Controllers;
    using Dotnetcorehack.Models;
    using Dotnetcorehack.Repositories;
    using Dotnetcorehack.Repositories.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class AnswerChoicesControllerTest
    {
        private Mock<IAnswerChoiceRepository> answerChoiceRepository;
        private AnswerChoicesController answerChoicesController;
        private int expectedId;
        private Random random;

        public AnswerChoicesControllerTest()
        {
            this.random = new Random();
            this.expectedId = this.random.Next();
            this.answerChoiceRepository = new Mock<IAnswerChoiceRepository>();

            this.answerChoicesController = new AnswerChoicesController(this.answerChoiceRepository.Object);
        }

        [Fact]
        public void ShouldDefineTheRoute()
        {
            var routeAttribute = typeof(AnswerChoicesController).GetTypeInfo().GetCustomAttributes(typeof(RouteAttribute), true)
                .Cast<RouteAttribute>()
                .FirstOrDefault();

            Assert.NotNull(routeAttribute);
            Assert.Equal(routeAttribute.Template, "/[controller]");
        }

        public class GetTest : AnswerChoicesControllerTest
        {
            [Fact]
            public void ShouldGetAAnswerChoiceByTheId()
            {
                var expectedAnswerChoice = new AnswerChoice();
                this.answerChoiceRepository.Setup(repo => repo.GetAnswerChoiceById(this.expectedId)).Returns(expectedAnswerChoice);

                var response = this.answerChoicesController.Get(this.expectedId) as JsonResult;

                Assert.Same(expectedAnswerChoice, response.Value);
            }

            [Fact]
            public void ShouldReturnANotFoundResponseWhenTheAnswerChoiceDoesNotExist()
            {
                var response = this.answerChoicesController.Get(this.expectedId);

                Assert.IsType<NotFoundResult>(response);
            }

            [Fact]
            public void ShouldBeAHttpGet()
            {
                var methodInfo = typeof(AnswerChoicesController).GetMethod(nameof(this.answerChoicesController.Get));

                var getAttribute = methodInfo.GetCustomAttributes(typeof(HttpGetAttribute), true)
                .Cast<HttpGetAttribute>()
                .FirstOrDefault();

                Assert.NotNull(getAttribute);
                Assert.Equal(getAttribute.Template, "{id}");
            }
        }

        public class PostTest : AnswerChoicesControllerTest
        {
            [Fact]
            public void ShouldCreateANewAnswerChoice()
            {
                var expectedAnswerChoiceToBeCreated = new AnswerChoice();
                var expectedAnswerChoiceCreated = new AnswerChoice();
                expectedAnswerChoiceCreated.Id = this.random.Next();

                this.answerChoiceRepository.Setup(repo => repo.CreateAnswerChoice(expectedAnswerChoiceToBeCreated)).Returns(expectedAnswerChoiceCreated);

                var actualResponse = this.answerChoicesController.Post(expectedAnswerChoiceToBeCreated) as CreatedResult;

                Assert.Equal(actualResponse.Location, "/answerChoices/" + expectedAnswerChoiceCreated.Id);
                Assert.Same(expectedAnswerChoiceCreated, actualResponse.Value);
            }

            [Fact]
            public void ShouldBeAHttpPost()
            {
                var methodInfo = typeof(AnswerChoicesController).GetMethod(nameof(this.answerChoicesController.Post));

                var postAttribute = methodInfo.GetCustomAttributes(typeof(HttpPostAttribute), true)
                .Cast<HttpPostAttribute>()
                .FirstOrDefault();

                Assert.NotNull(postAttribute);
            }
        }

        public class PutTest : AnswerChoicesControllerTest
        {
            [Fact]
            public void ShouldUpdateAAnswerChoice()
            {
                var expectedAnswerChoiceToBeUpdated = new AnswerChoice();

                this.answerChoiceRepository.Setup(repo => repo.UpdateAnswerChoice(this.expectedId, expectedAnswerChoiceToBeUpdated)).Returns(new AnswerChoice());

                var actualResponse = this.answerChoicesController.Put(this.expectedId, expectedAnswerChoiceToBeUpdated);

                Assert.IsType<NoContentResult>(actualResponse);
            }

            [Fact]
            public void ShouldReturnNotFoundWhenTheAnswerChoiceDoesNotExist()
            {
                var actualResponse = this.answerChoicesController.Put(this.expectedId, new AnswerChoice());

                Assert.IsType<NotFoundResult>(actualResponse);
            }

            [Fact]
            public void ShouldBeAHttpPut()
            {
                var methodInfo = typeof(AnswerChoicesController).GetMethod(nameof(this.answerChoicesController.Put));

                var putAttribute = methodInfo.GetCustomAttributes(typeof(HttpPutAttribute), true)
                .Cast<HttpPutAttribute>()
                .FirstOrDefault();

                Assert.NotNull(putAttribute);
                Assert.Equal(putAttribute.Template, "{id}");
            }
        }

        public class DeleteTest : AnswerChoicesControllerTest
        {
            [Fact]
            public void ShouldCallRepositoryWithId()
            {
                var actualResponse = this.answerChoicesController.Delete(this.expectedId);

                this.answerChoiceRepository.Verify(repo => repo.DeleteAnswerChoice(this.expectedId), Times.Once);

                Assert.IsType<NoContentResult>(actualResponse);
            }

            [Fact]
            public void ShouldBeAHttpDelete()
            {
                var methodInfo = typeof(AnswerChoicesController).GetMethod(nameof(this.answerChoicesController.Delete));

                var deleteAttribute = methodInfo.GetCustomAttributes(typeof(HttpDeleteAttribute), true)
                .Cast<HttpDeleteAttribute>()
                .FirstOrDefault();

                Assert.NotNull(deleteAttribute);
                Assert.Equal(deleteAttribute.Template, "{id}");
            }
        }
    }
}
