using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Repositories;
using MusicPortal.DAL.Models;
using MusicPortal.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicPortal.DAL.Repositories
{
    public class StyleRepository: IRepository<Style>
    {
        private MusicPortalContext db;

        public StyleRepository(MusicPortalContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Style>> GetAll()
        {
            return await db.Styles.ToListAsync();
        }

        public async Task<Style> Get(int id)
        {
            Style? style = await db.Styles.FindAsync(id);
            return style;
        }

        public async Task<Style> Get(string name)
        {
            var styles = await db.Styles.Where(a => a.Name == name).ToListAsync();
            Style? style = styles?.FirstOrDefault();
            return style;
        }

        public async Task Create(Style style)
        {
            await db.Styles.AddAsync(style);
        }

        public void Update(Style style)
        {
            db.Entry(style).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Style? style = await db.Styles.FindAsync(id);
            if (style != null)
                db.Styles.Remove(style);
        }


    }
}
