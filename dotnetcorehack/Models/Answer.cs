namespace Dotnetcorehack.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Answer
    {
        public int Id { get; set; }

        public int AnswerChoiceId { get; set; }
    }
}