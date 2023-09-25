using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Models;

namespace MusicPortal.DAL.Context
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
