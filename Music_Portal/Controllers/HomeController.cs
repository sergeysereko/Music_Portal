using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Music_Portal.Models;
using System.Diagnostics;

namespace Music_Portal.Controllers
{
    public class HomeController : Controller
    {

        MusicPortalContext db;
        IWebHostEnvironment _appEnvironment;

        public HomeController(MusicPortalContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index()
        {

            List<MusicFileShow> musicFileShows = new List<MusicFileShow>();

            using (var dbc = db)
            {
                var musicFiles = dbc.Music_files.Include(m => m.Style).Include(m => m.Singer).ToList();

                foreach (var musicFile in musicFiles)
                {
                    var music = new MusicFileShow
                    {
                        MusicFileName = musicFile.Name,
                        MusicFileSize = musicFile.Size,
                        StyleName = musicFile.Style?.Name,
                        SingerName = musicFile.Singer?.Name,
                        MusicFilePath = musicFile.File,
                        MusicFileId = musicFile.Id
                    };

                    musicFileShows.Add(music);
                }
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
            IEnumerable<Style> style = await Task.Run(() => db.Styles);
            ViewBag.Styles = style;
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> CreateStyle()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStyle(Style style)
        {

            if (ModelState.IsValid)
            {

                db.Add(style);
                db.SaveChangesAsync();
                return RedirectToAction("Styles");
            }
            else
            {
                return View(style);
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditStyle(int? id)
        {
            if (id == null || db.Styles == null)
            {
                return NotFound();
            }

            var style = await db.Styles.FindAsync(id);
            if (style == null)
            {
                return NotFound();
            }
            return View(style);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStyle(int id, [Bind("Id,Name,Music_file")] Style style)
        {
            if (id != style.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(style);
                    await db.SaveChangesAsync();
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
            return (db.Styles?.Any(e => e.Id == id)).GetValueOrDefault();
        }




        [HttpGet]
        public async Task<IActionResult> DeleteStyle(int? id)
        {
            if (id == null || db.Styles == null)
            {
                return NotFound();
            }

            var style = await db.Styles.FirstOrDefaultAsync(m => m.Id == id);
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
            if (db.Styles == null)
            {
                return Problem("Entity set 'MusicPortalContext.Style'  is null.");
            }
            var style = await db.Styles.FindAsync(id);
            if (style != null)
            {
                db.Styles.Remove(style);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Styles");
        }


        public async Task<IActionResult> Singers()
        {
            IEnumerable<Singer> singer = await Task.Run(() => db.Singers);
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
        public IActionResult CreateSinger(Singer singer)
        {

            if (ModelState.IsValid)
            {

                db.Add(singer);
                db.SaveChangesAsync();
                return RedirectToAction("Singers");
            }
            else
            {
                return View(singer);
            }

        }



        [HttpGet]
        public async Task<IActionResult> EditSinger(int? id)
        {
            if (id == null || db.Singers == null)
            {
                return NotFound();
            }

            var singer = await db.Singers.FindAsync(id);
            if (singer == null)
            {
                return NotFound();
            }
            return View(singer);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSinger(int id, [Bind("Id,Name,Music_file")] Singer singer)
        {
            if (id != singer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(singer);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StyleExists(singer.Id))
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
            return (db.Singers?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpGet]
        public async Task<IActionResult> DeleteSinger(int? id)
        {
            if (id == null || db.Singers == null)
            {
                return NotFound();
            }

            var singer = await db.Singers.FirstOrDefaultAsync(m => m.Id == id);
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
            if (db.Singers == null)
            {
                return Problem("Entity set 'MusicPortalContext.Singer'  is null.");
            }
            var singer = await db.Singers.FindAsync(id);
            if (singer != null)
            {
                db.Singers.Remove(singer);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Singers");
        }



        [HttpGet]
        public IActionResult CreateMusicFile()
        {
            var styles = db.Styles.ToList();
            var singers = db.Singers.ToList();

            ViewBag.StyleList = new SelectList(styles, "Id", "Name");
            ViewBag.SingerList = new SelectList(singers, "Id", "Name");

            return View("CreateMusicFile");
        }



        [HttpPost]
        [AllowAnonymous]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> CreateMusicFile(IFormFile FileMp3, [Bind("Id,Name,Id_Singer,Id_Style,Comment,FileMp3")] MusicFile_View mfile)
        {

            if (FileMp3 != null)
            {
               
                string path = "/File/" + FileMp3.FileName; 
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await FileMp3.CopyToAsync(fileStream); 
                }

                Music_file musicfile = new Music_file();
     
                musicfile.Name = mfile.Name;
                musicfile.Id_Singer = mfile.Id_Singer;
                musicfile.Id_Style = mfile.Id_Style;
                musicfile.Size = (((int)FileMp3.Length)/1000000).ToString() + " Mb";
                musicfile.Rating = 0;
                musicfile.File = path;
                

                string login = HttpContext.Session.GetString("login");
                if (!string.IsNullOrEmpty(login))
                {
                    User? user = await db.Users.FirstOrDefaultAsync(u => u.Name == login);
                    musicfile.Id_User = user.Id;
                    musicfile.User = user;
                }

                Style style = await db.Styles.FirstOrDefaultAsync(u => u.Id == mfile.Id_Style);
                if (style != null)
                {
                    musicfile.Style = style;
                }

                Singer singer = await db.Singers.FirstOrDefaultAsync(u => u.Id == mfile.Id_Singer);
                if (singer != null)
                {
                    musicfile.Singer = singer;
                }
       
                    db.Add(musicfile);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

            }

            return View(mfile);

        }


    }
}