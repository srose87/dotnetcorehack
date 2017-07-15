namespace Dotnetcorehack.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Phone { get; set; }
    }
}
