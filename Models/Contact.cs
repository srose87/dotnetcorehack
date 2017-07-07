using System.ComponentModel.DataAnnotations;

namespace dotnetcorehack.Models {
    public class Contact {
        
        public int id {get; set;}
        public string firstName {get; set;} 
        public string lastName {get; set;}
        public string phone {get; set;}

    }
}