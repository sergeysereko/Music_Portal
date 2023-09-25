using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Models;

namespace MusicPortal.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Music_file> Music_files { get; }
        IRepository<Singer> Singers { get; }
        IRepository<Style> Styles { get; }
        IRepository<User> Users {  get; }
        Task Save();
    }
}
