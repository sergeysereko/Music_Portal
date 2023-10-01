using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Models;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using AutoMapper;
using MusicPortal.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System.Numerics;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.Services
{

    public class MusicFileService : IMusicFileService
    {
        IUnitOfWork Database { get; set; }

        public MusicFileService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateMusicFile(Music_FileDTO mfDto)
        {
            var music_file = new Music_file
            {
                Id = mfDto.Id,
                Name = mfDto.Name,
                Size = mfDto.Size,
                Rating = mfDto.Rating,
                File = mfDto.File,
                Id_User = mfDto.Id_User,
                Id_Style = mfDto.Id_Style,
                Id_Singer = mfDto.Id_Singer,

            };
            await Database.Music_files.Create(music_file);
            await Database.Save();
        }

        public async Task UpdateMusicFile(Music_FileDTO mfDto)
        {
            var music_file = new Music_file
            {
                Id = mfDto.Id,
                Name = mfDto.Name,
                Size = mfDto.Size,
                Rating = mfDto.Rating,
                File = mfDto.File,
                Id_User = mfDto.Id_User,
                Id_Style = mfDto.Id_Style,
                Id_Singer = mfDto.Id_Singer

            };
            Database.Music_files.Update(music_file);
            await Database.Save();
        }

        public async Task DeleteMusicFile(int id)
        {
            await Database.Music_files.Delete(id);
            await Database.Save();
        }

        public async Task<Music_FileDTO> GetMusicFile(int id)
        {
            var music_file = await Database.Music_files.Get(id);
            if (music_file == null)
            {
                throw new ValidationException("Wrong Music_File!");
            }
            return new Music_FileDTO
            {
                Id = music_file.Id,
                Name = music_file.Name,
                Size = music_file.Size,
                Rating = music_file.Rating,
                File = music_file.File,
                Id_User = music_file.Id_User,
                Id_Style = music_file.Id_Style,
                Id_Singer = music_file.Id_Singer,
                User = music_file.User?.Name,
                Style = music_file.Style?.Name,
                Singer = music_file.Singer?.Name
            };
        }



        public async Task<IEnumerable<Music_FileDTO>> GetMusicFiles()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Music_file, Music_FileDTO>()
                   .ForMember("User", opt => opt.MapFrom(src => src.User.Name))
                   .ForMember("Style", opt => opt.MapFrom(src => src.Style.Name))
                   .ForMember("Singer", opt => opt.MapFrom(src => src.Singer.Name));
            });

            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Music_file>, IEnumerable<Music_FileDTO>>(await Database.Music_files.GetAll());
        }



    }

}