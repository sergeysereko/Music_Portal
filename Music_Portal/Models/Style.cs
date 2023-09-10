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
            this.Music_Item = new HashSet<Music_Item>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Music_Item> Music_Item { get; set; }
    }
}
