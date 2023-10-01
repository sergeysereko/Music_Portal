using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Music_Portal.Models;
using System.Diagnostics;
using System.Net;
using System.Text;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.Interfaces;



namespace Music_Portal.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMusicFileService musicFileService;
        private readonly ISingerService singerService;
        private readonly IStyleService styleService;
        IWebHostEnvironment _appEnvironment;


        public HomeController(IMusicFileService mfserv, ISingerService singerserv, IStyleService styleserv, IWebHostEnvironment appEnvironment)
        {
            musicFileService = mfserv;
            singerService = singerserv;
            styleService = styleserv;
            _appEnvironment = appEnvironment;
        }




        public async Task<IActionResult> Index()
        {
            
            List<MusicFileShow> musicFileShows = new List<MusicFileShow>();

        
                var musicFiles = await musicFileService.GetMusicFiles();
                var styles = await styleService.GetStyles();
                var singers = await singerService.GetSingers();

                var singerIds = musicFiles.Select(musicFile => musicFile.Id_Singer).Distinct();
                var singerPosters = singers.Where(singer => singerIds.Contains(singer.Id)).ToDictionary(singer => singer.Id, singer => singer.Poster);

                foreach (var musicFile in musicFiles)
                {
                var style = styles.FirstOrDefault(s => s.Id == musicFile.Id_Style);
                var singer = singers.FirstOrDefault(s => s.Id == musicFile.Id_Singer);


                    var music = new MusicFileShow
                    {
                        MusicFileName = musicFile.Name,
                        MusicFileSize = musicFile.Size,
                        StyleName = style?.Name,
                        SingerName = singer?.Name,
                        MusicFilePath = musicFile.File,
                        MusicFileId = musicFile.Id,
                        SingerPoster = singerPosters.ContainsKey(musicFile.Id_Singer) ? singerPosters[musicFile.Id_Singer] : null

                    };

                    musicFileShows.Add(music);
                }

            return View(musicFileShows);
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public async Task<IActionResult> Styles()
        {
            var styles = await styleService.GetStyles();
            ViewBag.Styles = styles;
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> CreateStyle()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStyle(StyleDTO style)
        {

            if (ModelState.IsValid)
            {
                await styleService.CreateStyle(style);
                return RedirectToAction("Styles");
            }
            else
            {
                return View(style);
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditStyle(int id)
        {
            
            if (id == null || await styleService.GetStyles() == null)
            {
                return NotFound();
            }
           
            var style = await styleService.GetStyle(id); 
            if (style == null)
            {
                return NotFound();
            }
            return View(style);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStyle(int id, [Bind("Id,Name,Music_file")] StyleDTO style)
        {
            if (id != style.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await styleService.UpdateStyle(style);
                 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StyleExists(style.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Styles");
            }
            return View(style);
        }

        private bool StyleExists(int id)
        {
            var styles = styleService.GetStyles().GetAwaiter().GetResult();
            return styles.Any(style => style.Id == id);
        }






        [HttpGet]
        public async Task<IActionResult> DeleteStyleView(int id)
        {
            if (id == null || await styleService.GetStyles() == null)
            {
                return NotFound();
            }

            var style = await styleService.GetStyle(id);
            
            if (style == null)
            {
                return NotFound();
            }

            return View(style);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStyle(int id)
        {
            if (styleService.GetStyles() == null)
            {
                return Problem("Entity set 'MusicPortalContext.Style'  is null.");
            }
            var style = await styleService.GetStyle(id);
            if (style != null)
            {  
                await styleService.DeleteStyle(id);
            }

            return RedirectToAction("Styles");
        }


        public async Task<IActionResult> Singers()
        { 
            var singer = await singerService.GetSingers();
            ViewBag.Singers = singer;
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> CreateSinger()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSinger(IFormFile Poster, [Bind("Id,Name,Poster")] SingerDTO singer)
        {
            if (Poster != null)
            {

                string path = "/Image/" + TransliterateToLatin(Poster.FileName);

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await Poster.CopyToAsync(fileStream);
                }

                singer.Poster = "~" + path;

            }

            if (ModelState.IsValid)
            {
                await singerService.CreateSinger(singer);
                return RedirectToAction("Singers");
            }
            else
            {
                return View(singer);
            }

        }



        [HttpGet]
        public async Task<IActionResult> EditSinger(int id)
        {
            if (id == null || await singerService.GetSingers() == null)
            {
                return NotFound();
            }

            var singer = await singerService.GetSinger(id);
           
            if (singer == null)
            {
                return NotFound();
            }
            return View(singer);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSinger(IFormFile Poster, int id, [Bind("Id,Name,Poster")] SingerDTO singer)
        {
            if (id != singer.Id)
            {
                return NotFound();
            }
            if (Poster != null)
            {
                string path = "/Image/" + TransliterateToLatin(Poster.FileName);

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await Poster.CopyToAsync(fileStream);
                }

                singer.Poster = path;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await singerService.UpdateSinger(singer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SingerExists(singer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Singers");
            }
            return View(singer);
        }

        private bool SingerExists(int id)
        {
            var singers = singerService.GetSingers().GetAwaiter().GetResult();
            return singers.Any(singer => singer.Id == id);  
        }


        [HttpGet]
        public async Task<IActionResult> DeleteSingerView(int id)
        {
            if (id == null || await singerService.GetSingers() == null)
            {
                return NotFound();
            }
            var singer = await singerService.GetSinger(id);  
            if (singer == null)
            {
                return NotFound();
            }

            return View(singer);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSinger(int id)
        {
            if (await singerService.GetSingers() == null)
            {
                return Problem("Entity set 'MusicPortalContext.Singer'  is null.");
            }
            var singer = await singerService.GetSinger(id); 
            if (singer != null)
            {
                await singerService.DeleteSinger(id);
            }
            return RedirectToAction("Singers");
        }



        [HttpGet]
        public async Task<IActionResult> CreateMusicFile()
        {
            //var styles = db.Styles.ToList();
            //var singers = db.Singers.ToList();
            var styles = await styleService.GetStyles();
            var singers = await singerService.GetSingers();
;
            ViewBag.StyleList = new SelectList(styles, "Id", "Name");
            ViewBag.SingerList = new SelectList(singers, "Id", "Name");

            return View("CreateMusicFile");
        }



        //    [HttpPost]
        //    [AllowAnonymous]
        //    [DisableRequestSizeLimit]
        //    public async Task<IActionResult> CreateMusicFile(IFormFile FileMp3, [Bind("Id,Name,Id_Singer,Id_Style,Comment,FileMp3")] MusicFile_View mfile)
        //    {

        //        if (FileMp3 != null)
        //        {

        //            string path = "/File/" + TransliterateToLatin(FileMp3.FileName);

        //            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
        //            {
        //                await FileMp3.CopyToAsync(fileStream);
        //            }

        //            Music_file musicfile = new Music_file();

        //            musicfile.Name = mfile.Name;
        //            musicfile.Id_Singer = mfile.Id_Singer;
        //            musicfile.Id_Style = mfile.Id_Style;
        //            musicfile.Size = (((int)FileMp3.Length) / 1000000).ToString() + " Mb";
        //            musicfile.Rating = 0;
        //            musicfile.File = path;


        //            string login = HttpContext.Session.GetString("login");
        //            if (!string.IsNullOrEmpty(login))
        //            {
        //                User? user = await db.Users.FirstOrDefaultAsync(u => u.Name == login);
        //                musicfile.Id_User = user.Id;
        //                musicfile.User = user;
        //            }

        //            Style style = await db.Styles.FirstOrDefaultAsync(u => u.Id == mfile.Id_Style);
        //            if (style != null)
        //            {
        //                musicfile.Style = style;
        //            }

        //            Singer singer = await db.Singers.FirstOrDefaultAsync(u => u.Id == mfile.Id_Singer);
        //            if (singer != null)
        //            {
        //                musicfile.Singer = singer;
        //            }

        //            db.Add(musicfile);
        //            await db.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));

        //        }

        //        return View(mfile);

        //    }



        //    [HttpGet]
        //    public async Task<IActionResult> EditMusicFile(int? id)
        //    {

        //        if (id == null || db.Music_files == null)
        //        {
        //            return NotFound();
        //        }


        //        var musicFile = await db.Music_files.FindAsync(id);
        //        if (musicFile == null)
        //        {
        //            return NotFound();
        //        }

        //        var styles = db.Styles.ToList();
        //        var singers = db.Singers.ToList();

        //        int selectedSingerId = musicFile.Id_Singer;
        //        int selectedStyleId = musicFile.Id_Style;
        //        ViewBag.StyleList = new SelectList(styles, "Id", "Name", selectedStyleId);
        //        ViewBag.SingerList = new SelectList(singers, "Id", "Name", selectedSingerId);

        //        var musicFileShow = new MusicFileShow
        //        {
        //            MusicFileId = musicFile.Id,
        //            MusicFileName = musicFile.Name,
        //            MusicFileSize = musicFile.Size,
        //            MusicFilePath = musicFile.File
        //        };

        //        return View(musicFileShow);

        //    }


        //    [HttpPost]
        //    [AllowAnonymous]
        //    [DisableRequestSizeLimit]
        //    public async Task<IActionResult> EditMusicFile(IFormFile MusicFilePath, [Bind("MusicFileName,MusicFileSize,StyleName,SingerName,MusicFilePath,MusicFileId")] MusicFileShow mfile)
        //    {
        //        if (MusicFilePath != null)
        //        {

        //            string path = "/File/" + TransliterateToLatin(MusicFilePath.FileName);
        //            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
        //            {
        //                await MusicFilePath.CopyToAsync(fileStream);
        //            }

        //            Music_file musicfile = new Music_file();

        //            musicfile.Id = mfile.MusicFileId;
        //            musicfile.Name = mfile.MusicFileName;
        //            musicfile.Size = (((int)MusicFilePath.Length) / 1000000).ToString() + " Mb";
        //            musicfile.Rating = 0;
        //            musicfile.File = path;
        //            int StyleId = int.Parse(mfile.StyleName);
        //            int SingerId = int.Parse(mfile.SingerName);

        //            string login = HttpContext.Session.GetString("login");
        //            if (!string.IsNullOrEmpty(login))
        //            {
        //                User? user = await db.Users.FirstOrDefaultAsync(u => u.Name == login);
        //                musicfile.Id_User = user.Id;
        //                musicfile.User = user;
        //            }

        //            Style style = await db.Styles.FirstOrDefaultAsync(u => u.Id == StyleId);
        //            if (style != null)
        //            {
        //                musicfile.Style = style;
        //                musicfile.Id_Style = style.Id;
        //            }

        //            Singer singer = await db.Singers.FirstOrDefaultAsync(u => u.Id == SingerId);
        //            if (singer != null)
        //            {
        //                musicfile.Singer = singer;
        //                musicfile.Id_Singer = singer.Id;
        //            }

        //            db.Update(musicfile);
        //            await db.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));

        //        }

        //        return View(mfile);

        //    }

        //    [HttpGet]
        //    public async Task<IActionResult> DeleteMusicFile(int? id)
        //    {

        //        if (id == null || db.Music_files == null)
        //        {
        //            return NotFound();
        //        }


        //        var musicFile = await db.Music_files.FindAsync(id);
        //        if (musicFile == null)
        //        {
        //            return NotFound();
        //        }

        //        Style style = await db.Styles.FirstOrDefaultAsync(u => u.Id == musicFile.Id_Style);
        //        Singer singer = await db.Singers.FirstOrDefaultAsync(u => u.Id == musicFile.Id_Singer);



        //        var musicFileShow = new MusicFileShow
        //        {
        //            MusicFileId = musicFile.Id,
        //            MusicFileName = musicFile.Name,
        //            MusicFileSize = musicFile.Size,
        //            MusicFilePath = musicFile.File,
        //            SingerName = singer.Name,
        //            StyleName = style.Name
        //        };

        //        return View(musicFileShow);
        //    }


        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteMusicFile(int id)
        //    {
        //        if (db.Music_files == null)
        //        {
        //            return Problem("Entity set 'MusicPortalContext.MusicFile'  is null.");
        //        }
        //        var musicFile = await db.Music_files.FindAsync(id);
        //        if (musicFile != null)
        //        {
        //            db.Music_files.Remove(musicFile);
        //        }

        //        await db.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }



        public static string TransliterateToLatin(string input)
        {
            Dictionary<char, string> transliteration = new Dictionary<char, string>
        {
            {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"}, {'е', "e"}, {'ё', "yo"},
            {'ж', "zh"}, {'з', "z"}, {'и', "i"}, {'й', "y"}, {'к', "k"}, {'л', "l"}, {'м', "m"},
            {'н', "n"}, {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"}, {'у', "u"},
            {'ф', "f"}, {'х', "kh"}, {'ц', "ts"}, {'ч', "ch"}, {'ш', "sh"}, {'щ', "sch"}, {'ъ', ""},
            {'ы', "y"}, {'ь', ""}, {'э', "e"}, {'ю', "yu"}, {'я', "ya"},
            {'А', "A"}, {'Б', "B"}, {'В', "V"}, {'Г', "G"}, {'Д', "D"}, {'Е', "E"}, {'Ё', "Yo"},
            {'Ж', "Zh"}, {'З', "Z"}, {'И', "I"}, {'Й', "Y"}, {'К', "K"}, {'Л', "L"}, {'М', "M"},
            {'Н', "N"}, {'О', "O"}, {'П', "P"}, {'Р', "R"}, {'С', "S"}, {'Т', "T"}, {'У', "U"},
            {'Ф', "F"}, {'Х', "Kh"}, {'Ц', "Ts"}, {'Ч', "Ch"}, {'Ш', "Sh"}, {'Щ', "Sch"}, {'Ъ', ""},
            {'Ы', "Y"}, {'Ь', ""}, {'Э', "E"}, {'Ю', "Yu"}, {'Я', "Ya"}
        };

            StringBuilder result = new StringBuilder(input.Length);

            foreach (char c in input)
            {
                if (transliteration.TryGetValue(c, out string transliteratedChar))
                {
                    result.Append(transliteratedChar);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }


    }

}