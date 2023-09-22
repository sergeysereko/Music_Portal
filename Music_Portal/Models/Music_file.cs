using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Music_Portal.Models
{
    public class Music_file
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [Display(Name = "Название")]
        public string Name { get; set; }
  
        
        [Display(Name = "Размер")]
        public string? Size { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [Range(1, 10, ErrorMessage = "Рейтинг должен быть в диапазоне от 1 до 10.")]
        [Display(Name = "Рейтинг")]
        public int Rating { get; set; }


        [Display(Name = "Муз.Файл")]
        public string? File { get; set; }


        [Display(Name = "Участник")]
        public int Id_User { get; set; }

        [Display(Name = "Исполнитель")]
        public int Id_Singer { get; set; }

        [Display(Name = "Стиль музыки")]
        public int Id_Style { get; set; }

        public Style Style { get; set; }

        public Singer Singer { get; set; }

        public User User { get; set; }


    }
}
