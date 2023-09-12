using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;



namespace Music_Portal.Models
{

    public class MusicPortalContext : DbContext
    {
        public MusicPortalContext(DbContextOptions<MusicPortalContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Music_file> Music_files { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Singer> Singers { get; set; }
    }
}

