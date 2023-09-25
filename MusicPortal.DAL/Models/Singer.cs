using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Models
{
    public class Singer
    {
        public Singer()
        {
            this.Music_file = new HashSet<Music_file>();
        }
        public int Id { get; set; }
       
        public string Name { get; set; }
        public string? Poster { get; set; }

        public ICollection<Music_file> Music_file { get; set; }
    }
}
