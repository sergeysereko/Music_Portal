using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace Music_Portal.Models
{
    public class Music_file
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Id_Singer { get; set; }

        public string Size { get; set; }

        public int Rating { get; set; }

        public string Path { get; set; }

        public int Id_Style { get; set; }

        public string Comment { get; set; }

        public int Id_User { get; set; }

        public Style Style { get; set; }

        public Singer Singer { get; set; }

        public User User { get; set; }
    }
}
