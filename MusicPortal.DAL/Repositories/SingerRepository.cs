using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Models;
using MusicPortal.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Repositories
{
    public class SingerRepository:IRepository<Singer>
    {
        private MusicPortalContext db;

        public SingerRepository(MusicPortalContext context)
        {
            this.db = context;
        }
    }
}
