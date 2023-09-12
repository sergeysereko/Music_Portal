using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Music_Portal.Models
{

    public class User
    {
        public User()
        {
            this.Music_file = new HashSet<Music_file>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int Access { get; set; }

        public ICollection<Music_file> Music_file { get; set; }
    }
}