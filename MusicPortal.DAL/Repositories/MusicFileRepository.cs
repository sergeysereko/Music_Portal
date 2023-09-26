using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Models;
using MusicPortal.DAL.Repositories;
using MusicPortal.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicPortal.DAL.Repositories
{
    public class MusicFileRepository:IRepository<Music_file>
    {
        private MusicPortalContext db;

        public MusicFileRepository(MusicPortalContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Music_file>> GetAll()
        {
            return await db.Music_files.ToListAsync();
        }

        public async Task<Music_file> Get(int id)
        {
            Music_file? mf = await db.Music_files.FindAsync(id);
            return mf;
        }

        public async Task<Music_file> Get(string name)
        {
            var mfs = await db.Music_files.Where(a => a.Name == name).ToListAsync();
            Music_file? mf = mfs?.FirstOrDefault();
            return mf;
        }

        public async Task Create(Music_file mf)
        {
            await db.Music_files.AddAsync(mf);
        }

        public void Update(Music_file mf)
        {
            db.Entry(mf).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Music_file? mf = await db.Music_files.FindAsync(id);
            if (mf != null)
                db.Music_files.Remove(mf);
        }
    }
}
