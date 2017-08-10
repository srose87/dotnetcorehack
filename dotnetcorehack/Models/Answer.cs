namespace Dotnetcorehack.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Answer
    {
        public int Id { get; set; }

        public int AnswerChoiceId { get; set; }

        public string FreeFormText { get; set; }

        [Required]
        public int QuestionId { get; set; }
    }
}