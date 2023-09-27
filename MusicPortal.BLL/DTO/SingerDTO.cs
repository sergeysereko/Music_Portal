using MusicPortal.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.DTO
{
    public class SingerDTO
    {    
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [Display(Name = "Название: ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [Display(Name = "Постер исполнителя: ")]
        public string? Poster { get; set; }

       
    }
}
