using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Models;
using AutoMapper;
using MusicPortal.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.Services
{
    public class StyleService
    {
        IUnitOfWork Database { get; set; }

        public StyleService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateStyle(StyleDTO styleDto)
        {
            var style = new Style
            {
                Id = styleDto.Id,
                Name = styleDto.Name              
            };
            await Database.Styles.Create(style);
            await Database.Save();
        }

        public async Task UpdateStyle(StyleDTO styleDto)
        {
            var style = new Style
            {
                Id = styleDto.Id,
                Name = styleDto.Name,            
            };
            Database.Styles.Update(style);
            await Database.Save();
        }

        public async Task DeleteStyle(int id)
        {
            await Database.Styles.Delete(id);
            await Database.Save();
        }

        public async Task<StyleDTO> GetStyle(int id)
        {
            var style = await Database.Styles.Get(id);
            if (style == null)
            {
                throw new ValidationException("Wrong team!");
            }
            return new StyleDTO
            {
                Id = style.Id,
                Name = style.Name            
            };
        }

        public async Task<IEnumerable<StyleDTO>> GetStyles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Style, StyleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Style>, IEnumerable<StyleDTO>>(await Database.Styles.GetAll());
        }

    }
}
