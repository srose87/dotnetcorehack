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

    public class SurveysQuestionsControllerTest
    {
        private Mock<IQuestionRepository> questionRepository;
        private SurveysQuestionsController surveysQuestionsController;
        private int expectedSurveyId;
        private Random random;

        public SurveysQuestionsControllerTest()
        {
            this.random = new Random();
            this.expectedSurveyId = this.random.Next();
            this.questionRepository = new Mock<IQuestionRepository>();

            this.surveysQuestionsController = new SurveysQuestionsController(this.questionRepository.Object);
        }

        public class GetTest : SurveysQuestionsControllerTest
        {
            [Fact]
            public void ShouldReturnSpecificQuestionsForASurvey()
            {
                var expectedQuestions = new List<Question>();
                expectedQuestions.Add(new Question());

                this.questionRepository.Setup(repo => repo.GetQuestionsFromSurveyId(this.expectedSurveyId)).Returns(expectedQuestions);

                var response = this.surveysQuestionsController.Get(this.expectedSurveyId) as JsonResult;

                Assert.Same(expectedQuestions, response.Value);
            }

            [Fact]
            public void ShouldDefineTheRoute()
            {
                var methodInfo = typeof(SurveysQuestionsController).GetMethod(nameof(this.surveysQuestionsController.Get));

                var routeAttribute = methodInfo.GetCustomAttributes(typeof(RouteAttribute), true)
                    .Cast<RouteAttribute>()
                    .FirstOrDefault();

                Assert.NotNull(routeAttribute);
                Assert.Equal(routeAttribute.Template, "/surveys/{id}/questions");
            }

            [Fact]
            public void ShouldReturnNotFoundIfNoQuestionsExistForTheSurvey()
            {
                var response = this.surveysQuestionsController.Get(this.expectedSurveyId);

                Assert.IsType<NotFoundResult>(response);
            }

            [Fact]
            public void ShouldBeAHttpGet()
            {
                var methodInfo = typeof(SurveysQuestionsController).GetMethod(nameof(this.surveysQuestionsController.Get));

                var getAttribute = methodInfo.GetCustomAttributes(typeof(HttpGetAttribute), true)
                .Cast<HttpGetAttribute>()
                .FirstOrDefault();

                Assert.NotNull(getAttribute);
            }
        }
    }
}
