using Microsoft.AspNetCore.Mvc;
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

    }
}