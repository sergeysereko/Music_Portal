using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Portal.Models;
using System.Security.Cryptography;
using System.Text;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Models;
using MusicPortal.DAL.Repositories;

namespace Music_Portal.Controllers
{
    public class LoginController : Controller
    {

        MusicPortalContext db;
        public LoginController(MusicPortalContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login logon)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                var users = db.Users.Where(a => a.Name == logon.UserName);
                if (users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                var user = users.First();
                string? salt = user.Salt;

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

                //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                HttpContext.Session.SetString("login", user.Name);
                HttpContext.Session.SetString("Access", user.Access.ToString());
                return RedirectToAction("Index", "Home");
            }
            return View(logon);
        }


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(Registration reg)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Name = reg.Login;
                user.Email = reg.Email;
                user.Access = 0;

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

                //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(reg);
        }


        public async Task<IActionResult> AllUsers()
        {
            IEnumerable<User> users = await Task.Run(() => db.Users);
            ViewBag.Users = users;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> EditAccess(int? id)
        {
            if (id == null || db.Users == null)
            {
                return NotFound();
            }

            var user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccess(int id, [Bind("Id,Name,Email,Password,Salt,Access,Music_file")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(user);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        private bool UserExists(int id)
        {
            return (db.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpGet]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null || db.Users == null)
            {
                return NotFound();
            }

            var user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (db.Users == null)
            {
                return Problem("Entity set 'MusicPortalContext.User'  is null.");
            }
            var user = await db.Users.FindAsync(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
