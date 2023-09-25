using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Models
{
    public class Music_file
    {
        public int Id { get; set; }
       
        public string Name { get; set; }

        public string? Size { get; set; }
  
        public int Rating { get; set; }
       
        public string? File { get; set; }

        public int Id_User { get; set; }

        public int Id_Singer { get; set; }
     
        public int Id_Style { get; set; }

        public Style Style { get; set; }

        public Singer Singer { get; set; }

        public User User { get; set; }

    }
}
