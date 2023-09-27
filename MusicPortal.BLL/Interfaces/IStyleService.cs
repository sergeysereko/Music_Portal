using MusicPortal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces
{
    public interface IStyleService
    {
        Task CreateStyle(StyleDTO styleDto);
        Task UpdateStyle(StyleDTO styleDto);
        Task DeleteStyle(int id);
        Task<SingerDTO> GetStyle(int id);
        Task<IEnumerable<SingerDTO>> GetStyles();
    }
}
