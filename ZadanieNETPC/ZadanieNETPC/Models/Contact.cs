using System.ComponentModel.DataAnnotations;

namespace ZadanieNETPC.Models
{
    //klasa z modelem danych
    public class Contact
    {
        [Key]
        public string Email { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Type { get; set; }
        public string PhoneNumb { get; set; }
        public string DateOfBirth { get; set; }

        public string Password { get; set; }
    }
}
