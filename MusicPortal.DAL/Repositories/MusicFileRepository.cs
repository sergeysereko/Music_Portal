using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Models;
using MusicPortal.DAL.Repositories;
using MusicPortal.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Repositories
{
    public class MusicFileRepository:IRepository<Music_file>
    {
        private MusicPortalContext db;

        public MusicFileRepository(MusicPortalContext context)
        {
            this.db = context;
        }
    }
}
