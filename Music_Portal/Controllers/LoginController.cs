using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Portal.Models;
using System.Security.Cryptography;
using System.Text;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Models;
using MusicPortal.DAL.Repositories;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Services;

namespace Music_Portal.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUserService userService;
        public LoginController(IUserService userserv)
        {
            userService = userserv;
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
        public async Task<IActionResult> Login(Login logon)
        {
            
            if (ModelState.IsValid)
            {
                if (await userService.GetUsers() == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                var users = await userService.GetUser(logon.UserName);
                if (users == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                UserDTO user = new UserDTO();
                user = users;
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
        public async Task<IActionResult> Registration(Registration reg)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO();
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
                await userService.CreateUser(user);

                return RedirectToAction("Login");
            }

            return View(reg);
        }


        public async Task<IActionResult> AllUsers()
        {
            var users = await userService.GetUsers();
            ViewBag.Users = users;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> EditAccess(int id)
        {
            if (id == null || await userService.GetUsers() == null)
            {
                return NotFound();
            }
            var user = await userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccess(int id, [Bind("Id,Name,Email,Password,Salt,Access,Music_file")] UserDTO user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await userService.UpdateUser(user);
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
                return RedirectToAction(nameof(AllUsers));
            }
            return View(user);
        }

        private bool UserExists(int id)
        {
            var users = userService.GetUsers().GetAwaiter().GetResult();
            return users.Any(user => user.Id == id);       
        }


        [HttpGet]
        public async Task<IActionResult> DeleteUserView(int id)
        {
            if (id == null || await userService.GetUsers() == null)
            {
                return NotFound();
            }
            var user = await userService.GetUser(id);
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
            if (await userService.GetUsers() == null)
            {
                return Problem("Entity set 'MusicPortalContext.User'  is null.");
            }
            var user = await userService.GetUser(id);
            if (user != null)
            {
                await userService.DeleteUser(id);
            }
            return RedirectToAction(nameof(AllUsers));
        }
    }
}
