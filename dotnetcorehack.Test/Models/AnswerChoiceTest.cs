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

    public class AnswerChoiceTest
    {
        private AnswerChoice answerChoice;
        private Faker faker;

        public AnswerChoiceTest()
        {
            this.faker = new Faker();
            this.answerChoice = new AnswerChoice();
        }

        [Fact]
        public void ShouldHaveAnIdProperty()
        {
            var expectedId = this.faker.Random.Int();
            this.answerChoice.Id = expectedId;

            Assert.Equal(this.answerChoice.Id, expectedId);
        }

        [Fact]
        public void ShouldHaveAQuestionIdProperty()
        {
            var expectedQuestionId = this.faker.Random.Int();
            this.answerChoice.QuestionId = expectedQuestionId;

            Assert.Equal(this.answerChoice.QuestionId, expectedQuestionId);
        }

        [Fact]
        public void ShouldHaveTheQuestionIdAsRequired()
        {
            var propertyInfo = typeof(AnswerChoice).GetProperty("QuestionId");

            var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), true)
                .Cast<RequiredAttribute>()
                .FirstOrDefault();

            Assert.NotNull(requiredAttribute);
        }

        [Fact]
        public void ShouldHaveALabelProperty()
        {
            var expectedLabel = this.faker.Lorem.Text();
            this.answerChoice.Label = expectedLabel;

            Assert.Equal(this.answerChoice.Label, expectedLabel);
        }

        [Fact]
        public void ShouldHaveTheLabelAsRequired()
        {
            var propertyInfo = typeof(AnswerChoice).GetProperty("Label");

            var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), true)
                .Cast<RequiredAttribute>()
                .FirstOrDefault();

            Assert.NotNull(requiredAttribute);
        }

        [Fact]
        public void ShouldHaveTheLabelWithAMaxLengthOf255()
        {
            var propertyInfo = typeof(AnswerChoice).GetProperty("Label");

            var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(MaxLengthAttribute), true)
                .Cast<MaxLengthAttribute>()
                .FirstOrDefault();

            Assert.NotNull(requiredAttribute);
            Assert.Equal(requiredAttribute.Length, 255);
        }
    }
}
