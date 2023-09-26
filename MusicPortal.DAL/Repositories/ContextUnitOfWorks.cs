using MusicPortal.DAL.Context;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Repositories;
using MusicPortal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Repositories
{
    public class ContextUnitOfWorks:IUnitOfWork
    {
        private MusicPortalContext db;
        private UserRepository userRep;
        private StyleRepository styleRep;
        private SingerRepository singerRep;
        private MusicFileRepository mfRep;

        public ContextUnitOfWorks(MusicPortalContext context)
        {
            db = context;
        }
        public IRepository<User> Users
        {
            get
            {
                if (userRep == null)
                {
                    userRep = new UserRepository(db);
                }
                return userRep;
            }
        }

        public IRepository<Style> Styles
        {
            get 
            {
                if (styleRep == null)
                {
                    styleRep = new StyleRepository(db);
                }
                return styleRep;
            }
        }

        public IRepository<Singer> Singers
        {
            get 
            {
                if (singerRep == null)
                {
                    singerRep = new SingerRepository(db);
                }
                return singerRep;
            }
        }

        public IRepository<Music_file> Music_files
        {
            get
            {
                if (mfRep == null)
                {
                    mfRep = new MusicFileRepository(db);
                }
                return mfRep;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

    }
}
