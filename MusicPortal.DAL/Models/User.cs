using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Models
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
