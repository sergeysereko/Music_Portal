using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Portal.Models;
using System.Diagnostics;

namespace Music_Portal.Controllers
{
    public class HomeController : Controller
    {


        MusicPortalContext db;
        public HomeController(MusicPortalContext context )
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
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

    }
}