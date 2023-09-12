using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace Music_Portal.Models
{
    public class Singer
    {
        public Singer()
        {
            this.Music_file = new HashSet<Music_file>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Music_file> Music_file { get; set; }
    }
}
