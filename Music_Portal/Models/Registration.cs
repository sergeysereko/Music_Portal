using System.ComponentModel.DataAnnotations;

namespace Music_Portal.Models
{
    public class Registration
    {

        [Required]

        [Display(Name = "Имя")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }

        [Required]
        [Display(Name = "Логин")]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
        
    }
}
