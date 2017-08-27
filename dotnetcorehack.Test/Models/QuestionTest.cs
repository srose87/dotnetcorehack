namespace Dotnetcorehack.Test.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using Bogus;
    using Dotnetcorehack.Models;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class QuestionTest
    {
        private Question question;
        private Faker faker;

        public QuestionTest()
        {
            this.faker = new Faker();
            this.question = new Question();
        }

        [Fact]
        public void ShouldHaveAnIdProperty()
        {
            var expectedQuestionId = this.faker.Random.Int();
            this.question.Id = expectedQuestionId;

            Assert.Equal(this.question.Id, expectedQuestionId);
        }

        [Fact]
        public void ShouldHaveATextProperty()
        {
            var expectedQuestionText = this.faker.Lorem.Text();
            this.question.Text = expectedQuestionText;

            Assert.Equal(this.question.Text, expectedQuestionText);
        }

        [Fact]
        public void ShouldHaveTheTextAsRequired()
        {
            var propertyInfo = typeof(Question).GetProperty(nameof(this.question.Text));

            var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), true)
                .Cast<RequiredAttribute>()
                .FirstOrDefault();

            Assert.NotNull(requiredAttribute);
        }

        [Fact]
        public void ShouldHaveTypeProperty()
        {
            var expectedQuestionType = this.faker.Lorem.Text();
            this.question.Type = expectedQuestionType;

            Assert.Equal(this.question.Type, expectedQuestionType);
        }

        [Fact]
        public void ShouldHaveTheTypeAsRequired()
        {
            var propertyInfo = typeof(Question).GetProperty(nameof(this.question.Type));

            var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), true)
                .Cast<RequiredAttribute>()
                .FirstOrDefault();

            Assert.NotNull(requiredAttribute);
        }

        [Fact]
        public void ShouldHaveSurveyIdProperty()
        {
            var expectedQuestionType = this.faker.Random.Int();
            this.question.SurveyId = expectedQuestionType;

            Assert.Equal(this.question.SurveyId, expectedQuestionType);
        }

        [Fact]
        public void ShouldHaveTheSurveyIdAsRequired()
        {
            var propertyInfo = typeof(Question).GetProperty(nameof(this.question.SurveyId));

            var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), true)
                .Cast<RequiredAttribute>()
                .FirstOrDefault();

            Assert.NotNull(requiredAttribute);
        }
    }
}
