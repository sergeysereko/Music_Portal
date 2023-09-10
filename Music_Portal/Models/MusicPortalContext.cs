using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace Music_Portal.Models
{
    public class MusicPortalContext: DbContext
    {
        public MusicPortalContext(DbContextOptions<MusicPortalContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Music_file> Music_file { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Style> Style { get; set; }
        public DbSet<Singer> Singer { get; set; }
    }
}


