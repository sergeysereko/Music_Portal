using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Models;
using System.Runtime.CompilerServices;
using MusicPortal.DAL.Context;

namespace MusicPortal.BLL.Infrastructure
{
    public static class MusicPortalContextExtensions
    {
        public static void AddMusicPortalContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MusicPortalContext>(options => options.UseSqlServer(connection));
        }
    }
}
