using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace Music_Portal.Models
{
    public class Style
    {
        public Style()
        {
            this.Music_file = new HashSet<Music_file>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [Display(Name = "Название ситля")]
        public string Name { get; set; }

        public ICollection<Music_file> Music_file { get; set; }
    }
}
