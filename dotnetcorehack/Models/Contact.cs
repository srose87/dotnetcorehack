using System.ComponentModel.DataAnnotations;

namespace dotnetcorehack.Models {
    public class Contact {
        
        public int id {get; set;}

        [Required]
        [MaxLength(255)]
        public string firstName {get; set;} 

        [Required]
        [MaxLength(255)]
        public string lastName {get; set;}
        
        [Required]
        [MaxLength(255)]
        public string phone {get; set;}

    }
}