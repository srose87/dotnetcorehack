namespace Dotnetcorehack.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dotnetcorehack.Controllers;
    using Dotnetcorehack.Models;
    using Dotnetcorehack.Repositories;
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

        private string AnyString()
        {
            return Guid.NewGuid().ToString();
        }

        private void GivenModelIsInvalid()
        {
            this.surveysController.ModelState.AddModelError(this.AnyString(), this.AnyString());
        }

        public class DeleteTest : SurveysControllerTest
        {
            [Fact]
            public void ShouldCallRepositoryWithId()
            {
                this.surveysController.Delete(this.expectedId);

                this.surveyRepository.Verify(repo => repo.DeleteSurvey(this.expectedId), Times.Once);
            }
        }

        public class PutTest : SurveysControllerTest
        {
            [Fact]
            public void ShouldReturnBadRequestWhenGivenInvalidModel()
            {
                this.GivenModelIsInvalid();

                var actualResponse = this.surveysController.Put(this.random.Next(), null);

                Assert.IsType<BadRequestObjectResult>(actualResponse);
            }

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
            public void ShouldReturnBadRequestWhenModelIsInvalid()
            {
                this.GivenModelIsInvalid();

                var actualResponse = this.surveysController.Post(null);

                Assert.IsType<BadRequestObjectResult>(actualResponse);
            }
        }

        public class GetTest : SurveysControllerTest
        {
            [Fact]
            public void ShouldReturnAllSurveys()
            {
                var expectedSurveysCount = this.random.Next(0, 100);
                var expectedSurveys = this.N(expectedSurveysCount, () => new Survey() { Id = this.random.Next() });
                this.surveyRepository.Setup(repo => repo.GetSurveys()).Returns(expectedSurveys);

                var response = this.surveysController.Get() as JsonResult;
                var actualSurveys = response.Value as List<Survey>;

                Assert.Same(expectedSurveys, actualSurveys);
                Assert.Equal(expectedSurveysCount, actualSurveys.Count);
            }

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

            private List<T> N<T>(int count, Func<T> creator)
            {
                var list = Enumerable.Range(0, count).Select(i => creator()).ToList();

                return list;
            }
        }
    }
}
