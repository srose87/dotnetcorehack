namespace Dotnetcorehack.Test.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using Bogus;
    using Dotnetcorehack.Models;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class SurveyTest
    {
        private Survey survey;
        private Faker faker;

        public SurveyTest()
        {
            this.faker = new Faker();
            this.survey = new Survey();
        }

        [Fact]
        public void ShouldHaveAnIdProperty()
        {
            var expectedSurveyId = this.faker.Random.Int();
            this.survey.Id = expectedSurveyId;

            Assert.Equal(this.survey.Id, expectedSurveyId);
        }

        [Fact]
        public void ShouldHaveATitleProperty()
        {
            var expectedSurveyText = this.faker.Lorem.Text();
            this.survey.Title = expectedSurveyText;

            Assert.Equal(this.survey.Title, expectedSurveyText);
        }

        [Fact]
        public void ShouldHaveTheTitleAsRequired()
        {
            var propertyInfo = typeof(Survey).GetProperty("Title");

            var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), true)
                .Cast<RequiredAttribute>()
                .FirstOrDefault();

            Assert.NotNull(requiredAttribute);
        }
    }
}
