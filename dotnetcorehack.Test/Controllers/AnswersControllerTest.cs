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

    public class AnswersControllerTest
    {
        private Mock<IAnswerRepository> answerRepository;
        private AnswersController answersController;
        private int expectedId;
        private Random random;

        public AnswersControllerTest()
        {
            this.random = new Random();
            this.expectedId = this.random.Next();
            this.answerRepository = new Mock<IAnswerRepository>();

            this.answersController = new AnswersController(this.answerRepository.Object);
        }

        [Fact]
        public void ShouldDefineTheRoute()
        {
            var routeAttribute = typeof(AnswersController).GetTypeInfo().GetCustomAttributes(typeof(RouteAttribute), true)
                .Cast<RouteAttribute>()
                .FirstOrDefault();

            Assert.NotNull(routeAttribute);
            Assert.Equal(routeAttribute.Template, "/[controller]");
        }

        public class GetTest : AnswersControllerTest
        {
            [Fact]
            public void ShouldGetAnswerByTheId()
            {
                var expectedAnswer = new Answer();
                this.answerRepository.Setup(repo => repo.GetAnswerById(this.expectedId)).Returns(expectedAnswer);

                var response = this.answersController.Get(this.expectedId) as JsonResult;

                Assert.Same(expectedAnswer, response.Value);
            }

            [Fact]
            public void ShouldReturnANotFoundResponseWhenTheAnswerDoesNotExist()
            {
                var response = this.answersController.Get(this.expectedId);

                Assert.IsType<NotFoundResult>(response);
            }

            [Fact]
            public void ShouldBeAHttpGet()
            {
                var methodInfo = typeof(AnswersController).GetMethod(nameof(this.answersController.Get));

                var getAttribute = methodInfo.GetCustomAttributes(typeof(HttpGetAttribute), true)
                .Cast<HttpGetAttribute>()
                .FirstOrDefault();

                Assert.NotNull(getAttribute);
                Assert.Equal(getAttribute.Template, "{id}");
            }
        }

        public class PostTest : AnswersControllerTest
        {
            [Fact]
            public void ShouldCreateANewAnswer()
            {
                var expectedAnswerToBeCreated = new Answer();
                var expectedAnswerCreated = new Answer();
                expectedAnswerCreated.Id = this.random.Next();

                this.answerRepository.Setup(repo => repo.CreateAnswer(expectedAnswerToBeCreated)).Returns(expectedAnswerCreated);

                var actualResponse = this.answersController.Post(expectedAnswerToBeCreated) as CreatedResult;

                Assert.Equal(actualResponse.Location, "/answers/" + expectedAnswerCreated.Id);
                Assert.Same(expectedAnswerCreated, actualResponse.Value);
            }

            [Fact]
            public void ShouldBeAHttpPost()
            {
                var methodInfo = typeof(AnswersController).GetMethod(nameof(this.answersController.Post));

                var postAttribute = methodInfo.GetCustomAttributes(typeof(HttpPostAttribute), true)
                .Cast<HttpPostAttribute>()
                .FirstOrDefault();

                Assert.NotNull(postAttribute);
            }
        }
    }
}
