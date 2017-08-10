namespace Dotnetcorehack.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int SurveyId { get; set; }
    }
}
