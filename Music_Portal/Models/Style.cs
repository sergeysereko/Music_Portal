using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Music_Portal.Models
{
    public class Style
    {
        public Style()
        {
            this.Music_file = new HashSet<Music_file>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Music_file> Music_file { get; set; }
    }
}
