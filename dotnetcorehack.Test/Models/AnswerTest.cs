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

    public class AnswerTest
    {
        private Answer answer;
        private Faker faker;

        public AnswerTest()
        {
            this.faker = new Faker();
            this.answer = new Answer();
        }

        [Fact]
        public void ShouldHaveAnIdProperty()
        {
            var expectedId = this.faker.Random.Int();
            this.answer.Id = expectedId;

            Assert.Equal(this.answer.Id, expectedId);
        }

        [Fact]
        public void ShouldHaveAnswerChoiceIdProperty()
        {
            var expectedAnswerChoiceId = this.faker.Random.Int();
            this.answer.AnswerChoiceId = expectedAnswerChoiceId;

            Assert.Equal(this.answer.AnswerChoiceId, expectedAnswerChoiceId);
        }

        [Fact]
        public void ShouldHaveQuestionChoiceIdProperty()
        {
            var expectedQuestionId = this.faker.Random.Int();
            this.answer.QuestionId = expectedQuestionId;

            Assert.Equal(this.answer.QuestionId, expectedQuestionId);
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
    }
}
