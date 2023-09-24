using System.ComponentModel.DataAnnotations;

namespace Music_Portal.Models
{
    public class MusicFileShow
    {
        [Display(Name = "Название")]
        public string MusicFileName { get; set; }

        [Display(Name = "Размер")]
        public string MusicFileSize { get; set; }

        [Display(Name = "Стиль")]
        public string StyleName { get; set; }

        [Display(Name = "Исполнитель")]
        public string SingerName { get; set; }

        [Display(Name = "Путь к файлу")]
        public string MusicFilePath { get; set; }

        [Display(Name = "Постер исполнителя")]
        public string SingerPoster { get; set; }

        public int MusicFileId { get; set; }
    }
}
