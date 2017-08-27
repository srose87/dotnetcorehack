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

    public class QuestionsControllerTest
    {
        private Mock<IQuestionRepository> questionRepository;
        private QuestionsController questionsController;
        private int expectedId;
        private Random random;

        public QuestionsControllerTest()
        {
            this.random = new Random();
            this.expectedId = this.random.Next();
            this.questionRepository = new Mock<IQuestionRepository>();

            this.questionsController = new QuestionsController(this.questionRepository.Object);
        }

        [Fact]
        public void ShouldDefineTheRoute()
        {
            var routeAttribute = typeof(QuestionsController).GetTypeInfo().GetCustomAttributes(typeof(RouteAttribute), true)
                .Cast<RouteAttribute>()
                .FirstOrDefault();

            Assert.NotNull(routeAttribute);
            Assert.Equal(routeAttribute.Template, "/[controller]");
        }

        public class GetTest : QuestionsControllerTest
        {
            [Fact]
            public void ShouldGetAQuestionByTheId()
            {
                var expectedQuestion = new Question();
                this.questionRepository.Setup(repo => repo.GetQuestionById(this.expectedId)).Returns(expectedQuestion);

                var response = this.questionsController.Get(this.expectedId) as JsonResult;

                Assert.Same(expectedQuestion, response.Value);
            }

            [Fact]
            public void ShouldReturnANotFoundResponseWhenTheQuestionDoesNotExist()
            {
                var response = this.questionsController.Get(this.expectedId);

                Assert.IsType<NotFoundResult>(response);
            }

            [Fact]
            public void ShouldBeAHttpGet()
            {
                var methodInfo = typeof(QuestionsController).GetMethod(nameof(this.questionsController.Get));

                var getAttribute = methodInfo.GetCustomAttributes(typeof(HttpGetAttribute), true)
                .Cast<HttpGetAttribute>()
                .FirstOrDefault();

                Assert.NotNull(getAttribute);
                Assert.Equal(getAttribute.Template, "{id}");
            }
        }

        public class PostTest : QuestionsControllerTest
        {
            [Fact]
            public void ShouldCreateANewQuestion()
            {
                var expectedQuestionToBeCreated = new Question();
                var expectedQuestionCreated = new Question();
                expectedQuestionCreated.Id = this.random.Next();

                this.questionRepository.Setup(repo => repo.CreateQuestion(expectedQuestionToBeCreated)).Returns(expectedQuestionCreated);

                var actualResponse = this.questionsController.Post(expectedQuestionToBeCreated) as CreatedResult;

                Assert.Equal(actualResponse.Location, "/questions/" + expectedQuestionCreated.Id);
                Assert.Same(expectedQuestionCreated, actualResponse.Value);
            }

            [Fact]
            public void ShouldBeAHttpPost()
            {
                var methodInfo = typeof(QuestionsController).GetMethod(nameof(this.questionsController.Post));

                var postAttribute = methodInfo.GetCustomAttributes(typeof(HttpPostAttribute), true)
                .Cast<HttpPostAttribute>()
                .FirstOrDefault();

                Assert.NotNull(postAttribute);
            }
        }

        public class PutTest : QuestionsControllerTest
        {
            [Fact]
            public void ShouldUpdateAQuestion()
            {
                var expectedQuestionToBeUpdated = new Question();

                this.questionRepository.Setup(repo => repo.UpdateQuestion(this.expectedId, expectedQuestionToBeUpdated)).Returns(new Question());

                var actualResponse = this.questionsController.Put(this.expectedId, expectedQuestionToBeUpdated);

                Assert.IsType<NoContentResult>(actualResponse);
            }

            [Fact]
            public void ShouldReturnNotFoundWhenTheQuestionDoesNotExist()
            {
                var actualResponse = this.questionsController.Put(this.expectedId, new Question());

                Assert.IsType<NotFoundResult>(actualResponse);
            }

            [Fact]
            public void ShouldBeAHttpPut()
            {
                var methodInfo = typeof(QuestionsController).GetMethod(nameof(this.questionsController.Put));

                var putAttribute = methodInfo.GetCustomAttributes(typeof(HttpPutAttribute), true)
                .Cast<HttpPutAttribute>()
                .FirstOrDefault();

                Assert.NotNull(putAttribute);
                Assert.Equal(putAttribute.Template, "{id}");
            }
        }

        public class DeleteTest : QuestionsControllerTest
        {
            [Fact]
            public void ShouldCallRepositoryWithId()
            {
                var actualResponse = this.questionsController.Delete(this.expectedId);

                this.questionRepository.Verify(repo => repo.DeleteQuestion(this.expectedId), Times.Once);

                Assert.IsType<NoContentResult>(actualResponse);
            }

            [Fact]
            public void ShouldBeAHttpDelete()
            {
                var methodInfo = typeof(QuestionsController).GetMethod(nameof(this.questionsController.Delete));

                var deleteAttribute = methodInfo.GetCustomAttributes(typeof(HttpDeleteAttribute), true)
                .Cast<HttpDeleteAttribute>()
                .FirstOrDefault();

                Assert.NotNull(deleteAttribute);
                Assert.Equal(deleteAttribute.Template, "{id}");
            }
        }
    }
}
