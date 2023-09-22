using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Music_Portal.Models
{
    public class MusicFile_View
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Исполнитель")]
        public int Id_Singer { get; set; }

        [Display(Name = "Стиль музыки")]
        public int Id_Style { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [Display(Name = "Описание трека")]
        public string? FileMp3 { get; set; }
    }
}
