namespace Dotnetcorehack.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Dotnetcorehack.Controllers;
    using Dotnetcorehack.Models;
    using Dotnetcorehack.Repositories;
    using Dotnetcorehack.Repositories.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class SurveysControllerTest
    {
        private Mock<ISurveyRepository> surveyRepository;
        private SurveysController surveysController;
        private int expectedId;
        private Random random;

        public SurveysControllerTest()
        {
            this.random = new Random();
            this.expectedId = this.random.Next();
            this.surveyRepository = new Mock<ISurveyRepository>();

            this.surveysController = new SurveysController(this.surveyRepository.Object);
        }

        [Fact]
        public void ShouldDefineTheRoute()
        {
            var routeAttribute = typeof(SurveysController).GetTypeInfo().GetCustomAttributes(typeof(RouteAttribute), true)
                .Cast<RouteAttribute>()
                .FirstOrDefault();

            Assert.NotNull(routeAttribute);
            Assert.Equal(routeAttribute.Template, "/[controller]");
        }

        public class DeleteTest : SurveysControllerTest
        {
            [Fact]
            public void ShouldCallRepositoryWithId()
            {
                this.surveysController.Delete(this.expectedId);

                this.surveyRepository.Verify(repo => repo.DeleteSurvey(this.expectedId), Times.Once);
            }

            [Fact]
            public void ShouldBeAHttpDelete()
            {
                var methodInfo = typeof(SurveysController).GetMethod(nameof(this.surveysController.Delete));

                var deleteAttribute = methodInfo.GetCustomAttributes(typeof(HttpDeleteAttribute), true)
                .Cast<HttpDeleteAttribute>()
                .FirstOrDefault();

                Assert.NotNull(deleteAttribute);
                Assert.Equal(deleteAttribute.Template, "{id}");
            }
        }

        public class PutTest : SurveysControllerTest
        {
            [Fact]
            public void ShouldReturnNoContentWhenSurveyUpdates()
            {
                var survey = new Survey();
                this.surveyRepository.Setup(repo => repo.UpdateSurvey(this.expectedId, survey)).Returns(new Survey());

                var actualResponse = this.surveysController.Put(this.expectedId, survey);

                Assert.IsType<NoContentResult>(actualResponse);
            }

            [Fact]
            public void ShouldReturnNotFoundWhenSurveyIsValidButDoesNotExist()
            {
                var actualResponse = this.surveysController.Put(this.expectedId, new Survey());

                Assert.IsType<NotFoundResult>(actualResponse);
            }

            [Fact]
            public void ShouldBeAHttpPut()
            {
                var methodInfo = typeof(SurveysController).GetMethod(nameof(this.surveysController.Put));

                var putAttribute = methodInfo.GetCustomAttributes(typeof(HttpPutAttribute), true)
                .Cast<HttpPutAttribute>()
                .FirstOrDefault();

                Assert.NotNull(putAttribute);
                Assert.Equal(putAttribute.Template, "{id}");
            }
        }

        public class PostTest : SurveysControllerTest
        {
            [Fact]
            public void ShouldCallRepoWithSurvey()
            {
                var survey = new Survey();
                this.surveyRepository.Setup(repo => repo.CreateSurvey(survey)).Callback(() => survey.Id = this.expectedId);

                var actualResponse = this.surveysController.Post(survey) as CreatedResult;

                Assert.NotNull(actualResponse);
                Assert.Equal(actualResponse.Location, "/surveys/" + this.expectedId);
            }

            [Fact]
            public void ShouldBeAHttpPost()
            {
                var methodInfo = typeof(SurveysController).GetMethod(nameof(this.surveysController.Post));

                var postAttribute = methodInfo.GetCustomAttributes(typeof(HttpPostAttribute), true)
                .Cast<HttpPostAttribute>()
                .FirstOrDefault();

                Assert.NotNull(postAttribute);
            }
        }

        public class GetTest : SurveysControllerTest
        {
            [Fact]
            public void ShouldReturnSpecificSurvey()
            {
                var expectedSurvey = new Survey();
                this.surveyRepository.Setup(repo => repo.GetSurveyById(this.expectedId)).Returns(expectedSurvey);

                var response = this.surveysController.Get(this.expectedId) as JsonResult;

                Assert.Same(expectedSurvey, response.Value);
            }

            [Fact]
            public void ShouldReturnNotFoundIfSurveyDoesNotExist()
            {
                var response = this.surveysController.Get(this.expectedId);

                Assert.IsType<NotFoundResult>(response);
            }

            [Fact]
            public void ShouldBeAHttpGet()
            {
                var methodInfo = typeof(SurveysController).GetMethod(nameof(this.surveysController.Get));

                var getAttribute = methodInfo.GetCustomAttributes(typeof(HttpGetAttribute), true)
                .Cast<HttpGetAttribute>()
                .FirstOrDefault();

                Assert.NotNull(getAttribute);
                Assert.Equal(getAttribute.Template, "{id}");
            }

            private List<T> N<T>(int count, Func<T> creator)
            {
                var list = Enumerable.Range(0, count).Select(i => creator()).ToList();

                return list;
            }
        }
    }
}
