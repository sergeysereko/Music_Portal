using MusicPortal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.Services
{
    public class SingerService:ISingerService
    {
        IUnitOfWork Database { get; set; }

        public SingerService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateSinger(SingerDTO singerDto)
        {
            var singer = new Singer
            {
                Id = singerDto.Id,
                Name = singerDto.Name,
                Poster = singerDto.Poster
            };
            await Database.Singers.Create(singer);
            await Database.Save();
        }

        public async Task UpdateSinger(SingerDTO singerDto)
        {
            var singer = new Singer
            {
                Id = singerDto.Id,
                Name = singerDto.Name,
                Poster = singerDto.Poster
            };
            Database.Singers.Update(singer);
            await Database.Save();
        }

        public async Task DeleteSinger(int id)
        {
            await Database.Singers.Delete(id);
            await Database.Save();
        }

        public async Task<SingerDTO> GetSinger(int id)
        {
            var singer = await Database.Singers.Get(id);
            if (singer == null)
            {
                throw new ValidationException("Wrong singer!");
            }
            return new SingerDTO
            {
                Id = singer.Id,
                Name = singer.Name,
                Poster = singer.Poster
            };
        }

       
        public async Task<IEnumerable<SingerDTO>> GetSingers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Singer, SingerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Singer>, IEnumerable<SingerDTO>>(await Database.Singers.GetAll());
        }

    }
}

