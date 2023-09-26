using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Models;
using MusicPortal.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicPortal.DAL.Repositories
{
    public class SingerRepository:IRepository<Singer>
    {
        private MusicPortalContext db;

        public SingerRepository(MusicPortalContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Singer>> GetAll()
        {
            return await db.Singers.ToListAsync();
        }

        public async Task<Singer> Get(int id)
        {
            Singer? singer = await db.Singers.FindAsync(id);
            return singer;
        }

        public async Task<Singer> Get(string name)
        {
            var singers = await db.Singers.Where(a => a.Name == name).ToListAsync();
            Singer? singer = singers?.FirstOrDefault();
            return singer;
        }

        public async Task Create(Singer singer)
        {
            await db.Singers.AddAsync(singer);
        }

        public void Update(Singer singer)
        {
            db.Entry(singer).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Singer? singer = await db.Singers.FindAsync(id);
            if (singer != null)
                db.Singers.Remove(singer);
        }

    }
}
