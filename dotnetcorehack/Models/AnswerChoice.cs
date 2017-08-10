namespace Dotnetcorehack.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AnswerChoice
    {
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Label { get; set; }
    }
}