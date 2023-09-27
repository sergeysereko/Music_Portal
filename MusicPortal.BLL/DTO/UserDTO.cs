using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [Display(Name = "Логин: ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Пароль: ")]
        public string Password { get; set; }
        public string Salt { get; set; }

        public int Access { get; set; }

    }
}
