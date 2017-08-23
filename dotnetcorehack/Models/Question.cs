namespace Dotnetcorehack.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net.Http;

    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int SurveyId { get; set; }

        public static implicit operator HttpContent(Question v)
        {
            throw new NotImplementedException();
        }
    }
}
