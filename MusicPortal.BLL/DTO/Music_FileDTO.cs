using MusicPortal.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.DTO
{
    public class Music_FileDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [Display(Name = "Название: ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [Display(Name = "Размер: ")]
        public string? Size { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [Display(Name = "Рейтинг: ")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [Display(Name = "Путь к файлу: ")]
        public string? File { get; set; }

        public int Id_User { get; set; }

        public int Id_Singer { get; set; }

        public int Id_Style { get; set; }

        public string Style { get; set; }

        public string Singer { get; set; }

        public string User { get; set; }

    }
}
