using System.ComponentModel.DataAnnotations;

namespace Music_Portal.Models
{
    public class Login
    {

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Имя пользователя: ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Пароль: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
