using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.BLL.DTO;

namespace MusicPortal.BLL.Interfaces
{
    public interface IMusicFileService
    {
        Task CreateMusicFile(Music_FileDTO mfDto);
        Task UpdateMusicFile(Music_FileDTO mfDto);
        Task DeleteMusicFile(int id);
        Task<Music_FileDTO> GetMusicFile(int id);
        Task<IEnumerable<Music_FileDTO>> GetMusicFiles();    
    }
}
