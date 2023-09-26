using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Repositories;
using MusicPortal.DAL.Models;
using MusicPortal.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Repositories
{
    public class StyleRepository: IRepository<Style>
    {
        private MusicPortalContext db;

        public StyleRepository(MusicPortalContext context)
        {
            this.db = context;
        }
    }
}
